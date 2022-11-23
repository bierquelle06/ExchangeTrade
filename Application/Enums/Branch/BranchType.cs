using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Branch.Enums
{
	public enum BranchType
	{
		[System.ComponentModel.Description("Unknown")]
		Unknown = -1,

		[System.ComponentModel.Description("Central")]
		Central = 1,

		[System.ComponentModel.Description("Branch")]
		Branch = 2
	}
}
