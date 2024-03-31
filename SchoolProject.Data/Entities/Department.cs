using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class Department : GeneralLocalizableEntity
	{
		public Department()
		{
			Students = new HashSet<Student>();
			DepartmentSubjects = new HashSet<DepartmentSubject>();
			Instructors = new HashSet<Instructor>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int DepartmentID { get; set; }
		public string? DepartmentNameAr { get; set; }
		public string? DepartmentNameEn { get; set; }
		public int? DepartmentManager { get; set; }
		[InverseProperty("Department")]
		public virtual ICollection<Student> Students { get; set; }
		[InverseProperty("Department")]
		public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
		[InverseProperty("department")]
		public virtual ICollection<Instructor> Instructors { get; set; }
		[ForeignKey(nameof(DepartmentManager))]
		[InverseProperty("departmentManager")]

		public virtual Instructor? Instructor { get; set; }
	}
}
