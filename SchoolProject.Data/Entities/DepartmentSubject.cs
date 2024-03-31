using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class DepartmentSubject
	{
		[Key]
		public int DepartmentID { get; set; }
		[Key]
		public int SubjectID { get; set; }
		[ForeignKey(nameof(DepartmentID))]
		[InverseProperty("DepartmentSubjects")]
		public virtual Department? Department { get; set; }
		[ForeignKey(nameof(SubjectID))]
		[InverseProperty("DepartmentsSubjects")]
		public virtual Subject? Subject { get; set; }
	}
}
