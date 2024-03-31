using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class Student : GeneralLocalizableEntity
	{
		public Student()
		{
			StudentSubject = new HashSet<StudentSubject>();
		}
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? NameAr { get; set; }
		[Display(Name = "Name")]
		public string? NameEn { get; set; }
		public string? Address { get; set; }
		public int? Age { get; set; }
		public string? Phone { get; set; }
		public int? DepartmentID { get; set; }
		[ForeignKey(nameof(DepartmentID))]
		[InverseProperty("Students")]
		public virtual Department? Department { get; set; }
		[InverseProperty("Student")]
		public virtual ICollection<StudentSubject> StudentSubject { get; set; }
	}
}
