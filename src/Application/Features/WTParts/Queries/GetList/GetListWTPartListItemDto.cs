﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WTParts.Queries.GetList;

public class GetListWTPartListItemDto
{
	public int LogID { get; set; }
	public string ParcaState { get; set; }
	public long? ParcaPartID { get; set; }
	public long? ParcaPartMasterID { get; set; }
	public string ParcaName { get; set; }
	public string ParcaNumber { get; set; }
	public string ParcaVersion { get; set; }
	public byte? EntegrasyonDurum { get; set; }
	public string LogMesaj { get; set; }
	public string Id { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
	public DateTime DeletedDate { get; set; }
}
