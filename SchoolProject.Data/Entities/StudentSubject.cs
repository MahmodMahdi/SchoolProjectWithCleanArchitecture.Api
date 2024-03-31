using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class StudentSubject
	{
		[Key]
		public int StudentID { get; set; }
		[Key]
		public int SubjectID { get; set; }
		public decimal? Grade { get; set; }
		[ForeignKey("StudentID")]
		[InverseProperty("StudentSubject")]
		public virtual Student? Student { get; set; }
		[ForeignKey("SubjectID")]
		[InverseProperty("StudentsSubjects")]
		public virtual Subject? Subject { get; set; }

	}
}
