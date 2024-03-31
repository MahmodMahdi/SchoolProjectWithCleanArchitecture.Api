using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class Subject : GeneralLocalizableEntity
	{
		public Subject()
		{
			StudentsSubjects = new HashSet<StudentSubject>();
			DepartmentsSubjects = new HashSet<DepartmentSubject>();
			InstructorSubjects = new HashSet<InstructorSubject>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SubjectID { get; set; }
		[StringLength(100)]
		public string? SubjectNameAr { get; set; }
		public string? SubjectNameEn { get; set; }
		public int? Period { get; set; }
		[InverseProperty("Subject")]
		public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
		[InverseProperty("Subject")]
		public virtual ICollection<DepartmentSubject> DepartmentsSubjects { get; set; }
		[InverseProperty("Subject")]
		public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }

	}
}
