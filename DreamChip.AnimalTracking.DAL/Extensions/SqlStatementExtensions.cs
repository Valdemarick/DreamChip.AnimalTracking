using System.Text;
using Microsoft.Extensions.Primitives;

namespace DreamChip.AnimalTracking.DAL.Extensions;

/// <summary>
/// Extensions that represents SQL statements.
/// </summary>
internal static class SqlStatementExtensions
{
    /// <summary>
    /// Represents SELECT statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="tableName">Table name for data selection.</param>
    /// <param name="columns">Selected columns.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder Select(this StringBuilder builder, string tableName, IEnumerable<string> columns)
    {
        builder.Append($@"SELECT {string.Join(',', columns.Select(x => $"\"{tableName}\".\"{x}\""))}")
            .Append('\n');

        return builder;
    }

    /// <summary>
    /// Represents FROM statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="tableName">Table name.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder From(this StringBuilder builder, string tableName)
    {
        builder.Append($"FROM \"{tableName}\"")
            .Append('\n');

        return builder;
    }

    /// <summary>
    /// Represents WHERE statement.
    /// </summary>
    /// <param name="builder">StringBuilder instance.</param>
    /// <param name="conditions">Select conditions.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder Where(this StringBuilder builder, IEnumerable<string> conditions)
    {
        if (!conditions.Any())
        {
            return builder;
        }
        
        builder.Append($@"WHERE {string.Join(" AND ", conditions)}")
            .Append('\n');

        return builder;
    }
    
    /// <summary>
    /// Represents WHERE statement.
    /// </summary>
    /// <param name="builder">StringBuilder instance.</param>
    /// <param name="condition">Select condition.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder Where(this StringBuilder builder, string condition)
    {
        builder.Append($@"WHERE {condition}")
            .Append('\n');

        return builder;
    }
    
    /// <summary>
    /// Represents ORDER BY ASC statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="orderColumn">Column to sort by.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder OrderByAscending(this StringBuilder builder, string orderColumn)
    {
        builder.Append($"ORDER BY {orderColumn}")
            .Append('\n');
        
        return builder;
    }

    /// <summary>
    /// Represents LEFT JOIN statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="leftTable">Left table name.</param>
    /// <param name="rightTable">Right table name.</param>
    /// <param name="leftColumn">Left column name.</param>
    /// <param name="rightColumn">Right column name.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder LeftJoin(
        this StringBuilder builder, 
        string leftTable, 
        string rightTable,
        string leftColumn,
        string rightColumn)
    {
        builder.Append($"LEFT JOIN \"{rightTable}\" ON \"{leftTable}\".\"{leftColumn}\" = \"{rightTable}\".\"{rightColumn}\"")
            .Append('\n');
        
        return builder;
    }

    /// <summary>
    /// Represents LEFT JOIN statement, that joins subquery.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="leftTable">Left table name.</param>
    /// <param name="subQuery">SQL subquery script.</param>
    /// <param name="alias">Alias of subquery table.</param>
    /// <param name="leftColumn">Left column name.</param>
    /// <param name="rightColumn">Right column name.</param>
    /// <returns>StringBuilder with LEFT JOIN statement.</returns>
    internal static StringBuilder LeftJoinSubQuery(
        this StringBuilder builder,
        string leftTable,
        string subQuery,
        string alias,
        string leftColumn,
        string rightColumn)
    {
        builder.Append($"LEFT JOIN {subQuery} AS \"{alias}\" ON \"{leftTable}\".\"{leftColumn}\" = \"{alias}\".\"{rightColumn}\"")
            .Append('\n');

        return builder;
    }

    /// <summary>
    /// Represents RIGHT JOIN statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="leftTable">Left table name.</param>
    /// <param name="rightTable">Right table name.</param>
    /// <param name="leftColumn">Left column name.</param>
    /// <param name="rightColumn">Right column name.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder RightJoin(
        this StringBuilder builder,
        string leftTable,
        string rightTable,
        string leftColumn,
        string rightColumn)
    {
        builder.Append($"RIGHT JOIN \"{rightTable}\" ON \"{leftTable}\".\"{leftColumn}\" = \"{rightTable}\".\"{rightColumn}\"")
            .Append('\n');
        
        return builder;
    }

    /// <summary>
    /// Represents INNER JOIN statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="leftTable">Left table name.</param>
    /// <param name="rightTable">Right table name.</param>
    /// <param name="leftColumn">Left column name.</param>
    /// <param name="rightColumn">Right column name.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder InnerJoin(
        this StringBuilder builder,
        string leftTable,
        string rightTable,
        string leftColumn,
        string rightColumn)
    {
        builder.Append($"INNER JOIN \"{rightTable}\" ON \"{leftTable}\".\"{leftColumn}\" = \"{rightTable}\".\"{rightColumn}\"")
            .Append('\n');
        
        return builder;
    }

    /// <summary>
    /// Represents ORDER BY DESC statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="orderColumn">Column to sort by.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder OrderByDescending(this StringBuilder builder, string orderColumn)
    {
        builder.Append($"ORDER BY \"{orderColumn}\" DESC")
            .Append('\n');
        
        return builder;
    }

    /// <summary>
    /// Represents INSERT statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="tableName">Table name to insert data.</param>
    /// <param name="columns">Columns with new data.</param>
    /// <returns>StringBuilder.</returns>
    internal static StringBuilder Insert(this StringBuilder builder, string tableName, IEnumerable<string> columns)
    {
        builder.Append($"INSERT INTO \"{tableName}\" ({string.Join(',', columns.Select(x => $"\"{x}\""))}) VALUES ({string.Join(',', columns.Select(x => $"@{x}"))})")
            .Append('\n');

        return builder;
    }

    /// <summary>
    /// Represents UPDATE statement.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tableName">Updated table name.</param>
    /// <param name="columns">Columns with new data.</param>
    /// <returns>String Builder with UPDATE statement.</returns>
    internal static StringBuilder Update(this StringBuilder builder, string tableName, IEnumerable<string> columns)
    {
        builder.Append($"UPDATE \"{tableName}\" SET {string.Join(',', columns.Select(x => $"\"{x}\" =" + $"@{x}"))}")
            .Append('\n');
        
        return builder;
    }

    /// <summary>
    /// Represents DELETE statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="tableName">Table name.</param>
    /// <returns>StringBuilder with the DELETE statement.</returns>
    internal static StringBuilder Delete(this StringBuilder builder, string tableName)
    {
        builder.Append($"DELETE FROM \"{tableName}\"")
            .Append('\n');

        return builder;
    }

    /// <summary>
    /// Represents OFFSET statement.
    /// </summary>
    /// <param name="builder">StringBuilder.</param>
    /// <param name="from">Started index.</param>
    /// <param name="size">The size of selection.</param>
    /// <returns>String Builder with OFFSET statement.</returns>
    internal static StringBuilder Offset(this StringBuilder builder, int from, int size)
    {
        builder.Append($"OFFSET {from} ROWS FETCH NEXT {size} ROWS ONLY")
            .Append('\n');

        return builder;
    }

    /// <summary>
    /// Adds additional columns to SELECT statement from another table.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tableName">Another table name.</param>
    /// <param name="columns">Another table columns.</param>
    /// <returns></returns>
    internal static StringBuilder AddColumns(this StringBuilder builder, string tableName, IEnumerable<string> columns)
    {
        builder.Append(',')
            .Append($"{string.Join(',', columns.Select(x => $"\"{tableName}\".\"{x}\""))}")
            .Append('\n');
        
        return builder;
    }

    internal static StringBuilder ReturningId(this StringBuilder builder)
    {
        builder.Append($" RETURNING \"Id\"")
            .Append('\n');

        return builder;
    }
}
