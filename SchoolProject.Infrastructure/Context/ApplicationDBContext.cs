using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Context
{
	public class ApplicationDBContext : IdentityDbContext<User, Role, int,
																IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		private readonly IEncryptionProvider _encryptionProvider;
		public ApplicationDBContext() { }
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
			_encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
		}
		public DbSet<User> User { get; set; }
		public DbSet<UserRefreshToken> userRefreshTokens { get; set; }
		public DbSet<Student> students { get; set; }
		public DbSet<Department> departments { get; set; }
		public DbSet<Subject> subjects { get; set; }
		public DbSet<StudentSubject> studentSubjects { get; set; }
		public DbSet<DepartmentSubject> departmentSubjects { get; set; }
		public DbSet<Instructor> instructors { get; set; }
		public DbSet<InstructorSubject> instructorSubjects { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DepartmentSubject>()
				.HasKey(x => new { x.SubjectID, x.DepartmentID });
			modelBuilder.Entity<StudentSubject>()
				.HasKey(x => new { x.SubjectID, x.StudentID });
			modelBuilder.Entity<InstructorSubject>()
				.HasKey(x => new { x.SubjectId, x.InstructorId });

			modelBuilder.Entity<Instructor>()
				.HasOne(x => x.Supervisor)
				.WithMany(x => x.Instructors)
				.HasForeignKey(x => x.SuperVisorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Department>()
				.HasOne(x => x.Instructor)
				.WithOne(x => x.departmentManager)
				.HasForeignKey<Department>(x => x.DepartmentManager)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.UseEncryption(_encryptionProvider);
			base.OnModelCreating(modelBuilder);
		}

	}
}
