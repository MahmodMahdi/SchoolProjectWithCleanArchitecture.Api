using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class InstructorSubject
	{
		[Key]
		public int InstructorId { get; set; }
		[Key]
		public int SubjectId { get; set; }
		[ForeignKey(nameof(InstructorId))]
		[InverseProperty("InstructorSubjects")]
		public virtual Instructor? instructor { get; set; }
		[ForeignKey(nameof(SubjectId))]
		[InverseProperty("InstructorSubjects")]
		public virtual Subject? Subject { get; set; }
	}
}
