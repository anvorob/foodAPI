using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RecipeAPI.Models
{
    public partial class RecipeapiContext : DbContext
    {
        public RecipeapiContext()
        {
        }

        public RecipeapiContext(DbContextOptions<RecipeapiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryToRecipe> CategoryToRecipe { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Diet> Diet { get; set; }
        public virtual DbSet<DietToRecipe> DietToRecipe { get; set; }
        public virtual DbSet<Favourite> Favourite { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<MealType> MealType { get; set; }
        public virtual DbSet<MealTypeToRecipe> MealTypeToRecipe { get; set; }
        public virtual DbSet<Nutrition> Nutrition { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=APLAKL-TECLT14\\SQLEXPRESS01;Initial Catalog=recipeapi;Integrated Security=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CategoryToRecipe>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.CategoryId })
                    .HasName("PK__Category__5C491B7218892DBC");

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryToRecipe)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CategoryT__Categ__33D4B598");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.CategoryToRecipe)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CategoryT__Recip__32E0915F");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment1)
                    .HasColumnName("comment")
                    .HasMaxLength(15);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Recipe).HasColumnName("recipe");

                entity.Property(e => e.User).HasColumnName("user");

                entity.HasOne(d => d.RecipeNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.Recipe)
                    .HasConstraintName("FK__Comment__recipe__3F466844");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK__Comment__user__3E52440B");
            });

            modelBuilder.Entity<Diet>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<DietToRecipe>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.DietId })
                    .HasName("PK__DietToRe__076DA7BB6C06E0C1");

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.DietId).HasColumnName("DietID");

                entity.HasOne(d => d.Diet)
                    .WithMany(p => p.DietToRecipe)
                    .HasForeignKey(d => d.DietId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DietToRec__DietI__398D8EEE");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.DietToRecipe)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DietToRec__Recip__38996AB5");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.UserId })
                    .HasName("PK__Favourit__2CA1041A65E8AC8C");

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Favourite)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__Recip__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourite)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__UserI__4316F928");
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Product)
                    .HasColumnName("product")
                    .HasMaxLength(30);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Recipe).HasColumnName("recipe");

                entity.Property(e => e.Units)
                    .HasColumnName("units")
                    .HasMaxLength(10);

                entity.HasOne(d => d.RecipeNavigation)
                    .WithMany(p => p.IngredientsNavigation)
                    .HasForeignKey(d => d.Recipe)
                    .HasConstraintName("FK__Ingredien__recip__25869641");
            });

            modelBuilder.Entity<MealType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<MealTypeToRecipe>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.MealTypeId })
                    .HasName("PK__MealType__0ADB3BAB3F301498");

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.MealTypeId).HasColumnName("MealTypeID");

                entity.HasOne(d => d.MealType)
                    .WithMany(p => p.MealTypeToRecipe)
                    .HasForeignKey(d => d.MealTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealTypeT__MealT__2E1BDC42");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.MealTypeToRecipe)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealTypeT__Recip__2D27B809");
            });

            modelBuilder.Entity<Nutrition>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(20);

                entity.Property(e => e.Label)
                    .HasColumnName("label")
                    .HasMaxLength(20);

                entity.Property(e => e.Recipe).HasColumnName("recipe");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasMaxLength(10);

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.RecipeNavigation)
                    .WithMany(p => p.Nutrition)
                    .HasForeignKey(d => d.Recipe)
                    .HasConstraintName("FK__Nutrition__recip__286302EC");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CookTime).HasColumnName("cookTime");

                entity.Property(e => e.Cuisine)
                    .HasColumnName("cuisine")
                    .HasMaxLength(30);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(100);

                entity.Property(e => e.Ingredients)
                    .HasColumnName("ingredients")
                    .HasMaxLength(500);

                entity.Property(e => e.Instructions)
                    .HasColumnName("instructions")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberOfServings).HasColumnName("numberOfServings");

                entity.Property(e => e.PrepTime).HasColumnName("prepTime");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(100);

                entity.Property(e => e.IsLoggedIn)
                    .HasColumnName("isLoggedIn")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(40);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(40);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
