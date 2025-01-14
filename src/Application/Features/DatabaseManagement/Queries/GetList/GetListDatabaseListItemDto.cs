﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DatabaseManagement.Queries.GetList;

public class GetListDatabaseListItemDto
{


	public string TableTitle { get; set; }
	public string TableName { get; set; }
	public string TableSchema { get; set; }
	public string CreateQuery { get; set; }
	public bool IsActive { get; set; }

	public GetListDatabaseListItemDto()
	{
		
	}

	public GetListDatabaseListItemDto(string tableTitle, string tableName, string tableSchema, string createQuery, bool ısActive)
	{
		TableTitle = tableTitle;
		TableName = tableName;
		TableSchema = tableSchema;
		CreateQuery = createQuery;
		IsActive = ısActive;
	}
}
