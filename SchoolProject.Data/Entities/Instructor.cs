using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class Instructor : GeneralLocalizableEntity
	{
		public Instructor()
		{
			Instructors = new HashSet<Instructor>();
			InstructorSubjects = new HashSet<InstructorSubject>();

		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int InstructorId { get; set; }
		public string? NameAr { get; set; }
		public string? NameEn { get; set; }
		public string? Address { get; set; }
		public string? Phone { get; set; }
		public int? SuperVisorId { get; set; }
		public decimal? Salary { get; set; }
		public string? Image { get; set; }
		public int? DepartmentId { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		[InverseProperty("Instructors")]
		public Department? department { get; set; }

		[InverseProperty("Instructor")]
		public Department? departmentManager { get; set; }

		[ForeignKey(nameof(SuperVisorId))]
		[InverseProperty("Instructors")]
		public Instructor? Supervisor { get; set; }


		[InverseProperty("Supervisor")]
		public virtual ICollection<Instructor> Instructors { get; set; }

		[InverseProperty("instructor")]
		public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }

	}
}
