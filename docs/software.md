# Реалізація інформаційного та програмного забезпечення
 
## SQL-скрипт для створення на початкового наповнення бази даних

```mysql

-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema OpenDataModel
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `OpenDataModel` ;

-- -----------------------------------------------------
-- Schema OpenDataModel
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `OpenDataModel` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_520_ci ;
USE `OpenDataModel` ;

-- -----------------------------------------------------
-- Table `OpenDataModel`.`Categoty`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Categoty` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Categoty` (
  `category_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `parent_category_id` INT NULL,
  PRIMARY KEY (`category_id`),
  INDEX `parent_category_idx` (`parent_category_id` ASC) INVISIBLE,
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE,
  CONSTRAINT `parent_category`
    FOREIGN KEY (`parent_category_id`)
    REFERENCES `OpenDataModel`.`Categoty` (`category_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Tag`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Tag` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Tag` (
  `tag_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`tag_id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Data`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Data` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Data` (
  `data_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `description` LONGTEXT NULL,
  `format` VARCHAR(45) NOT NULL,
  `content` VARCHAR(45) NOT NULL,
  `createdAt` DATETIME NOT NULL,
  `updatedAt` DATETIME NOT NULL,
  `category_id` INT NOT NULL,
  PRIMARY KEY (`data_id`, `category_id`),
  INDEX `fk_Data_Categoty_idx` (`category_id` ASC) VISIBLE,
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE,
  CONSTRAINT `fk_Data_Categoty`
    FOREIGN KEY (`category_id`)
    REFERENCES `OpenDataModel`.`Categoty` (`category_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Link`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Link` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Link` (
  `link_id` INT NOT NULL AUTO_INCREMENT,
  `data_id` INT NOT NULL,
  `tag_id` INT NOT NULL,
  PRIMARY KEY (`link_id`, `data_id`, `tag_id`),
  INDEX `fk_Link_Data_idx` (`data_id` ASC) VISIBLE,
  INDEX `fk_Link_Tag_idx` (`tag_id` ASC) VISIBLE,
  CONSTRAINT `fk_Link_Data`
    FOREIGN KEY (`data_id`)
    REFERENCES `OpenDataModel`.`Data` (`data_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Link_Tag`
    FOREIGN KEY (`tag_id`)
    REFERENCES `OpenDataModel`.`Tag` (`tag_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Role`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Role` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Role` (
  `role_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`role_id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`User` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`User` (
  `user_id` INT NOT NULL AUTO_INCREMENT,
  `firstname` VARCHAR(45) NOT NULL,
  `lastname` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `login` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE,
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Access`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Access` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Access` (
  `access_id` INT NOT NULL AUTO_INCREMENT,
  `data_id` INT NOT NULL,
  `role_id` INT NOT NULL,
  `user_id` INT NOT NULL,
  PRIMARY KEY (`access_id`, `data_id`, `user_id`, `role_id`),
  INDEX `fk_Access_Data_idx` (`data_id` ASC) VISIBLE,
  INDEX `fk_Access_Role_idx` (`role_id` ASC) VISIBLE,
  INDEX `fk_Access_User_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_Access_Data`
    FOREIGN KEY (`data_id`)
    REFERENCES `OpenDataModel`.`Data` (`data_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Access_Role`
    FOREIGN KEY (`role_id`)
    REFERENCES `OpenDataModel`.`Role` (`role_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Access_User`
    FOREIGN KEY (`user_id`)
    REFERENCES `OpenDataModel`.`User` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Categoty`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Categoty` (`category_id`, `name`, `parent_category_id`) VALUES (DEFAULT, 'Географія', NULL);
INSERT INTO `OpenDataModel`.`Categoty` (`category_id`, `name`, `parent_category_id`) VALUES (DEFAULT, 'Статистика', NULL);

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Tag`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Tag` (`tag_id`, `name`) VALUES (DEFAULT, 'Тег: статистика');
INSERT INTO `OpenDataModel`.`Tag` (`tag_id`, `name`) VALUES (DEFAULT, 'Тег: географія');

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Data`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Data` (`data_id`, `name`, `description`, `format`, `content`, `createdAt`, `updatedAt`, `category_id`) VALUES (DEFAULT, 'Статистика', 'Важлива статистика', 'txt', 'txt', '2038-01-19 03:14:07', '2039-01-19 03:14:07', 1);
INSERT INTO `OpenDataModel`.`Data` (`data_id`, `name`, `description`, `format`, `content`, `createdAt`, `updatedAt`, `category_id`) VALUES (DEFAULT, 'Географія', 'Важливі дані', 'png', 'png', '2027-01-19 03:14:07', '2030-01-19 03:14:07', 2);

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Link`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Link` (`link_id`, `data_id`, `tag_id`) VALUES (DEFAULT, 1, 1);
INSERT INTO `OpenDataModel`.`Link` (`link_id`, `data_id`, `tag_id`) VALUES (DEFAULT, 2, 2);

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Role`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Role` (`role_id`, `name`) VALUES (DEFAULT, 'Користувач');
INSERT INTO `OpenDataModel`.`Role` (`role_id`, `name`) VALUES (DEFAULT, 'Адміністратор');

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`User`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`User` (`user_id`, `firstname`, `lastname`, `email`, `login`, `password`) VALUES (DEFAULT, 'Іван', 'Рєзник', 'rieznyk@gmail.com', 'ivan', '1234');
INSERT INTO `OpenDataModel`.`User` (`user_id`, `firstname`, `lastname`, `email`, `login`, `password`) VALUES (DEFAULT, 'Нікіта', 'Пляко', 'plyako@gmail.com', 'nikito4ka', '5678');

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Access`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Access` (`access_id`, `data_id`, `role_id`, `user_id`) VALUES (DEFAULT, 1, 1, 1);
INSERT INTO `OpenDataModel`.`Access` (`access_id`, `data_id`, `role_id`, `user_id`) VALUES (DEFAULT, 2, 2, 2);

COMMIT;

```

## RESTfull сервіс для управління даними

### Основна точка входу для ASP.NET Core веб-додатка

```csharp
using DBLaba6.MainDB;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenDataManagement", Version = "v1" });
});

builder.Services.AddDbContext<OpendatamodelContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenDataManagement V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```


### Модель даних для роботи з таблицею Data
```csharp
using System.ComponentModel.DataAnnotations;


namespace DBLaba6.Controllers
{
    public class DataModel
    {
        [Required(ErrorMessage = "Data ID is required.")]
        public int DataId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Format is required.")]
        [StringLength(50, ErrorMessage = "Format cannot exceed 50 characters.")]
        public string Format { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Created At date is required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Updated date is required.")]
        public DateTime UpdatedAt { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }
    }
}
```

### Модель даних для роботи з таблицею User
```csharp
using System.ComponentModel.DataAnnotations;


namespace DBLaba6.Controllers
{
    public class UserModel
    {
        [Required(ErrorMessage = "User Id is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
```

### Контролер для виконання CRUD-операцій над користувачами
```csharp
using Microsoft.AspNetCore.Mvc;
using DBLaba6.MainDB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DBLaba6.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly OpendatamodelContext _context;

        public DataController(OpendatamodelContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var data = await _context.Data.ToListAsync();

            return Ok(data);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetDataId(int id)
        {
            var data = await _context.Data.Where(x => x.DataId == id).FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound($"Data with current ID '{id}' wasn't found in the database");
            }

            return Ok(data);
        }


        [HttpDelete("id")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var deldata = await _context.Data.Where(x => x.DataId == id).FirstOrDefaultAsync();

            if (deldata == null)
            {
                return NotFound("Data with such ID doesn't exist");
            }

            _context.Data.Remove(deldata);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Data deleted successfully.", userId = id });
        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] DataModel data)
        {
            if (!ModelState.IsValid)
            {
                var errorMesgs = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return BadRequest(string.Join(", ", errorMesgs));
            }

            var existingData = await _context.Data.FirstOrDefaultAsync(x => x.DataId == data.DataId);

            if (existingData != null)
            {
                return Conflict("A data with the same id already exists.");
            }

            var newData = new Datum
            {
                DataId = data.DataId,
                Name = data.Name,
                Description = data.Description,
                Format = data.Format,
                Content = data.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CategoryId = data.CategoryId
            };

            await _context.Data.AddAsync(newData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddData), new { id = newData.DataId }, newData);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateData([FromBody] DataModel data)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(new { errors });
            }

            var existData = await _context.Data.FirstOrDefaultAsync(x => x.DataId == data.DataId);

            if (existData == null)
            {
                return NotFound($"Data with ID '{data.DataId}' not found.");
            }

            existData.DataId = data.DataId;
            existData.Name = data.Name;
            existData.Description = data.Description;
            existData.Format = data.Format;
            existData.Content = data.Content;
            existData.CreatedAt = DateTime.UtcNow;
            existData.UpdatedAt = DateTime.UtcNow;
            existData.CategoryId = data.CategoryId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Data updated successfully.", DataId = data.DataId });
        }
    }
}
```

### Контролер для виконання CRUD-операцій над даними
```csharp
using Microsoft.AspNetCore.Mvc;
using DBLaba6.MainDB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DBLaba6.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly OpendatamodelContext _context;

        public UserController(ILogger<UserController> logger, OpendatamodelContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetUserId(int id)
        {
            var user = await _context.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound($"User with current ID '{id}' wasn't found in the database");
            }

            return Ok(user);
        }


        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deluser = await _context.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();

            if (deluser == null)
            {
                return NotFound("The user with such ID doesn't exist");
            }

            _context.Users.Remove(deluser);

            await _context.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully.", userId = id });
        }


        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                var errorMesgs = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return BadRequest(string.Join(", ", errorMesgs));
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login);

            if (existingUser != null)
            {
                return Conflict("A user with the same login already exists.");
            }

            var newUser = new User
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password 
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddUser), new { id = newUser.UserId }, newUser);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(new { errors });
            }

            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);

            if (existUser == null)
            {
                return NotFound($"User with ID '{user.UserId}' not found.");
            }

            var existLogin = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.UserId != user.UserId);

            if (existLogin != null)
            {
                return Conflict($"A user with the login '{user.Login}' already exists.");
            }

            existUser.Firstname = user.Firstname;
            existUser.Lastname = user.Lastname;
            existUser.Email = user.Email;
            existUser.Login = user.Login;
            existUser.Password = user.Password;

            await _context.SaveChangesAsync();

            return Ok(new { message = "User updated successfully.", userId = user.UserId });
        }
    }
}
```

### Клас, який визначає структуру для взаємодії з базою даних MySQL
```csharp
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBLaba6.MainDB;

public partial class OpendatamodelContext : DbContext
{
    public OpendatamodelContext()
    {
    }

    public OpendatamodelContext(DbContextOptions<OpendatamodelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<Categoty> Categoties { get; set; }

    public virtual DbSet<Datum> Data { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; } 

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=opendatamodel;uid=root;password=Rgrichell24", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_520_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => new { e.AccessId, e.DataId, e.UserId, e.RoleId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

            entity.ToTable("access");

            entity.HasIndex(e => e.DataId, "fk_Access_Data_idx");

            entity.HasIndex(e => e.RoleId, "fk_Access_Role_idx");

            entity.HasIndex(e => e.UserId, "fk_Access_User_idx");

            entity.Property(e => e.AccessId)
                .ValueGeneratedOnAdd()
                .HasColumnName("access_id");
            entity.Property(e => e.DataId).HasColumnName("data_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Data).WithMany(p => p.Accesses)
                .HasForeignKey(d => d.DataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Access_Data");

            entity.HasOne(d => d.Role).WithMany(p => p.Accesses)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Access_Role");
        });

        modelBuilder.Entity<Categoty>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categoty");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.HasIndex(e => e.ParentCategoryId, "parent_category_idx");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("parent_category");
        });

        modelBuilder.Entity<Datum>(entity =>
        {
            entity.HasKey(e => e.DataId).HasName("PRIMARY");

            entity.ToTable("data");

            entity.HasIndex(e => e.CategoryId, "fk_Data_Categoty_idx");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.DataId).HasColumnName("data_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Content)
                .HasMaxLength(45)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Format)
                .HasMaxLength(45)
                .HasColumnName("format");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Category).WithMany(p => p.Data)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Data_Categoty");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => new { e.LinkId, e.DataId, e.TagId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("link");

            entity.HasIndex(e => e.DataId, "fk_Link_Data_idx");

            entity.HasIndex(e => e.TagId, "fk_Link_Tag_idx");

            entity.Property(e => e.LinkId)
                .ValueGeneratedOnAdd()
                .HasColumnName("link_id");
            entity.Property(e => e.DataId).HasColumnName("data_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Data).WithMany(p => p.Links)
                .HasForeignKey(d => d.DataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Link_Data");

            entity.HasOne(d => d.Tag).WithMany(p => p.Links)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Link_Tag");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PRIMARY");

            entity.ToTable("tag");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Login, "login_UNIQUE").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(45)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(45)
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(45)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
```
