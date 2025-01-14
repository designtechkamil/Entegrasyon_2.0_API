﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DatabaseManagement;

public class DatabaseManagementDefinations
{


	public string TableTitle { get; set; }
	public string TableName { get; set; }
	public string TableSchema { get; set; }
	public string CreateQuery { get; set; }
	public bool IsActive { get; set; }

	public DatabaseManagementDefinations(string tableTitle,string tableName, string tableSchema, string createQuery, bool ısActive)
	{
		TableTitle = tableTitle;
		TableName = tableName;
		TableSchema = tableSchema;
		CreateQuery = createQuery;
		IsActive = ısActive;
	}



}
