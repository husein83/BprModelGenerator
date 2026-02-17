using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace ModelGenerator.Utility
{
    internal class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<EntityRefSetStructureModel> LoadStructure()
        {
            var result = new List<EntityRefSetStructureModel>();

            const string query =
            @"
				WITH ForeignKeys AS 
                (
                	SELECT 
                		fkc.parent_object_id,
                		fkc.parent_column_id AS col,
                		sRef.name AS RefSchema,
                		OBJECT_NAME(fkc.referenced_object_id) AS RefTable,
                		colP.name AS RefField,
                		CONCAT(OBJECT_NAME(fkc.referenced_object_id), 'Ref', colP.name) AS RefItem,
                		CONCAT('FK_', OBJECT_NAME(fkc.parent_object_id), '_', colP.name, '_2_', OBJECT_NAME(fkc.referenced_object_id), '_', colR.name) AS InverseKey
                	FROM sys.foreign_key_columns AS fkc
                	JOIN sys.foreign_keys AS fk ON fk.object_id = fkc.constraint_object_id
                	JOIN sys.columns AS colP ON colP.object_id = fkc.parent_object_id AND colP.column_id = fkc.parent_column_id
                	JOIN sys.columns AS colR ON colR.object_id = fkc.referenced_object_id AND colR.column_id = fkc.referenced_column_id
                	JOIN sys.tables AS tRef ON tRef.object_id = fkc.referenced_object_id
                	JOIN sys.schemas AS sRef ON sRef.schema_id = tRef.schema_id
                ), 
                RelationKeys AS (
                	SELECT
                		fkc.referenced_object_id AS ref_obj,
                		fkc.referenced_column_id AS ref_col,
                		sParent.name AS SchemaName,
                		OBJECT_NAME(fkc.parent_object_id) AS TableName,
                		colP.name AS Field,
                		CONCAT('FK_', OBJECT_NAME(fkc.parent_object_id), '_', colP.name, '_2_', OBJECT_NAME(fkc.referenced_object_id), '_', colR.name) AS InverseKey,
                		CASE
                			WHEN i.index_id IS NOT NULL AND idxCols.ColCount = 1 THEN CONCAT(OBJECT_NAME(fkc.parent_object_id), 'Ref', colP.name)
                			ELSE CONCAT(OBJECT_NAME(fkc.parent_object_id), 'Set', colP.name)
                		END AS SetField
                	FROM sys.foreign_key_columns AS fkc
                	JOIN sys.foreign_keys AS fk ON fk.object_id = fkc.constraint_object_id
                	JOIN sys.columns AS colP ON colP.object_id = fkc.parent_object_id AND colP.column_id = fkc.parent_column_id
                	JOIN sys.columns AS colR ON colR.object_id = fkc.referenced_object_id AND colR.column_id = fkc.referenced_column_id
                	JOIN sys.tables AS tParent ON tParent.object_id = fkc.parent_object_id
                	JOIN sys.schemas AS sParent ON sParent.schema_id = tParent.schema_id
                	LEFT JOIN sys.index_columns AS ic ON ic.object_id = fkc.parent_object_id AND ic.column_id = fkc.parent_column_id
                	LEFT JOIN (
                		SELECT object_id, index_id, COUNT(*) AS ColCount
                		FROM sys.index_columns
                		GROUP BY object_id, index_id
                	) AS idxCols ON idxCols.object_id = ic.object_id AND idxCols.index_id = ic.index_id
                	LEFT JOIN sys.indexes AS i ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                )
                
                SELECT 
                	s.name AS SchemaName,
                	t.name AS TableName,
                	c.name AS Field,
                
                	-- JSON of FK reference
                	(
                		SELECT 
                			fk.RefSchema AS [Schema],
                			fk.RefItem AS [Field],
                			fk.InverseKey AS [InverseKey]
                		FROM ForeignKeys fk
                		WHERE fk.parent_object_id = t.object_id AND fk.col = c.column_id
                		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
                	) AS RefJson,
                
                	-- JSON of reverse relations (Set)
                	ISNULL((
                		SELECT 
                			rk.SchemaName AS [Schema],
                			rk.SetField AS [Field],
                			rk.InverseKey AS [InverseKey]
                		FROM RelationKeys rk
                		WHERE rk.ref_obj = t.object_id AND rk.ref_col = c.column_id
                		FOR JSON PATH
                	), '[]') AS SetJson,
                
                	-- Bit flags
                	CASE WHEN fk.col IS NOT NULL THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS IsForeignKey,
                	CASE WHEN EXISTS (
                		SELECT 1 FROM RelationKeys rk WHERE rk.ref_obj = t.object_id AND rk.ref_col = c.column_id
                	) THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS IsRelationKey,
                	CASE WHEN idx.column_id IS NOT NULL THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS IsIndexedRelation,
                
                	-- Aggregated RefItems
                	(
                		SELECT STRING_AGG(fk.RefItem, ', ')
                		FROM ForeignKeys fk
                		WHERE fk.parent_object_id = t.object_id AND fk.col = c.column_id
                	) AS RefItem,
                
                	-- Aggregated SetFields
                	(
                		SELECT STRING_AGG(rk.SetField, ', ')
                		FROM RelationKeys rk
                		WHERE rk.ref_obj = t.object_id AND rk.ref_col = c.column_id
                	) AS SetItem
                
                FROM sys.schemas AS s
                JOIN sys.tables AS t ON t.schema_id = s.schema_id
                JOIN sys.columns AS c ON c.object_id = t.object_id
                
                -- FK lookup for IsForeignKey
                LEFT JOIN ForeignKeys fk ON fk.parent_object_id = t.object_id AND fk.col = c.column_id
                
                -- Index check
                LEFT JOIN (
                	SELECT ic.object_id, ic.column_id
                	FROM sys.index_columns AS ic
                	JOIN sys.indexes AS i 
                		ON i.object_id = ic.object_id AND i.index_id = ic.index_id AND i.is_hypothetical = 0
                ) AS idx ON idx.object_id = t.object_id AND idx.column_id = c.column_id
                
                -- Filter only rows with FK or Relation usage
                WHERE EXISTS (
                	SELECT 1 FROM ForeignKeys fk2 WHERE fk2.parent_object_id = t.object_id AND fk2.col = c.column_id
                	UNION
                	SELECT 1 FROM RelationKeys rk2 WHERE rk2.ref_obj = t.object_id AND rk2.ref_col = c.column_id
                )
                
                ORDER BY s.name, t.name, c.column_id;
			";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();
            using var reader = command.ExecuteReader();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            while (reader.Read())
            {
                var model = new EntityRefSetStructureModel
                {
                    SchemaName = reader["SchemaName"].ToString(),
                    TableName = reader["TableName"].ToString(),
                    Field = reader["Field"].ToString(),
                    IsForeignKey = Convert.ToBoolean(reader["IsForeignKey"]),
                    IsRelationKey = Convert.ToBoolean(reader["IsRelationKey"]),
                    IsIndexedRelation = Convert.ToBoolean(reader["IsIndexedRelation"]),
                    RefItem = reader["RefItem"] == DBNull.Value ? null : reader["RefItem"].ToString(),
                    SetItem = reader["SetItem"] == DBNull.Value ? null : reader["SetItem"].ToString(),
                };

                // Deserialize RefJson (if not null or empty)
                var refJsonStr = reader["RefJson"]?.ToString();
                if (!string.IsNullOrWhiteSpace(refJsonStr) && refJsonStr != "null")
                {
                    try
                    {
                        model.RefJson = JsonSerializer.Deserialize<RefSetJsonModel>(refJsonStr, options);
                    }
                    catch (JsonException)
                    {
                        throw;
                    }
                }

                // Deserialize SetJson (array of objects)
                var setJsonStr = reader["SetJson"]?.ToString();
                if (!string.IsNullOrWhiteSpace(setJsonStr) && setJsonStr != "null")
                {
                    try
                    {
                        model.SetJson = JsonSerializer.Deserialize<List<RefSetJsonModel>>(setJsonStr, options);
                    }
                    catch (JsonException)
                    {
                        throw;
                    }
                }

                result.Add(model);
            }

            return result;
        }

        public HashSet<string> LoadTableNameWithSchema()
        {
            var result = new HashSet<string>();

            const string query =
            @"
				SELECT 
                    CONCAT(
                        DB_NAME(), '.',          -- Current database name
                        s.name, '.',             -- Schema name
                        'DataModel.',            -- Static string
                        o.name                   -- Table or view name
                    ) AS FullName
                FROM sys.objects o
                JOIN sys.schemas s ON o.schema_id = s.schema_id
                WHERE o.type IN ('U', 'V')       -- U = User table, V = View
                ORDER BY FullName;
			";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var fullName = reader["FullName"]?.ToString();
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    result.Add(fullName);
                }
            }

            return result;
        }
    }
}
