using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BusinessObject
{
    public partial class MiniStoreContext : DbContext
    {
        public MiniStoreContext()
        {
        }

        public MiniStoreContext(DbContextOptions<MiniStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Barcode> Barcodes { get; set; }
        public virtual DbSet<Bonu> Bonus { get; set; }
        public virtual DbSet<BonusCriterion> BonusCriteria { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeBonu> EmployeeBonus { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Origin> Origins { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<SalaryReport> SalaryReports { get; set; }
        public virtual DbSet<ShiftConfig> ShiftConfigs { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<VoucherUsed> VoucherUseds { get; set; }
        public virtual DbSet<WorkShift> WorkShifts { get; set; }
        public virtual DbSet<WorkShiftEmployee> WorkShiftEmployees { get; set; }
        public virtual DbSet<Worksheet> Worksheets { get; set; }

        private string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Use the base directory of the application
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            var strConn = config["ConnectionStrings:MiniStore"];
            return strConn;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Barcode>(entity =>
            {
                entity.ToTable("Barcode");

                entity.Property(e => e.BarcodeId).HasColumnName("BarcodeID");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");

                entity.Property(e => e.ImportedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SoldDate).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Barcodes)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Barcode_Product");
            });

            modelBuilder.Entity<Bonu>(entity =>
            {
                entity.HasKey(e => e.BonusId)
                    .HasName("PK__Bonus__8E5547085E67C2EF");

                entity.Property(e => e.BonusId).HasColumnName("BonusID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Criteria)
                    .WithMany(p => p.Bonus)
                    .HasForeignKey(d => d.CriteriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bonus_BonusCriteria");
            });

            modelBuilder.Entity<BonusCriterion>(entity =>
            {
                entity.HasKey(e => e.CriteriaId)
                    .HasName("PK__BonusCri__FE6ADB2DA74F791C");

                entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FunctionName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brand_Country");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastBuyDate).HasColumnType("datetime");

                entity.Property(e => e.MemberSince).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("Employee");

                entity.HasIndex(e => e.UserName, "Employee_pk")
                    .IsUnique();

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.FirstDateAtWork).HasColumnType("date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PosId).HasColumnName("PosID");

                entity.Property(e => e.SalaryUpdateDate).HasColumnType("date");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pos)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Position");
            });

            modelBuilder.Entity<EmployeeBonu>(entity =>
            {
                entity.HasKey(e => e.QualifyId)
                    .HasName("PK__Employee__872DF3C418EDEFD4");

                entity.Property(e => e.QualifyId).HasColumnName("QualifyID");

                entity.Property(e => e.BonusId).HasColumnName("BonusID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.Period).HasColumnType("date");

                entity.HasOne(d => d.Bonus)
                    .WithMany(p => p.EmployeeBonus)
                    .HasForeignKey(d => d.BonusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeBonus_Bonus");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmployeeBonus)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeBonus_Employee");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.SalerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Saler)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Employee");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK__OrderDet__135C314DB99C9DD5");

                entity.ToTable("OrderDetail");

                entity.Property(e => e.DetailId).HasColumnName("DetailID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<Origin>(entity =>
            {
                entity.ToTable("Origin");

                entity.Property(e => e.OriginId).HasColumnName("OriginID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Origins)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Origin_Country");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PosId);

                entity.ToTable("Position");

                entity.Property(e => e.PosId).HasColumnName("PosID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.OriginId).HasColumnName("OriginID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Location");

                entity.HasOne(d => d.Origin)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.OriginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Origin");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DetailReason).HasMaxLength(300);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.ApproverNavigation)
                    .WithMany(p => p.RequestApproverNavigations)
                    .HasForeignKey(d => d.Approver)
                    .HasConstraintName("FK_Request_Employee");

                entity.HasOne(d => d.RequesterNavigation)
                    .WithMany(p => p.RequestRequesterNavigations)
                    .HasForeignKey(d => d.Requester)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Employee1");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salary");

                entity.HasIndex(e => new { e.EmpId, e.Amount, e.SalaryUpdateDate }, "CK_Salary")
                    .IsUnique();

                entity.Property(e => e.SalaryId).HasColumnName("SalaryID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.SalaryUpdateDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salary_Employee");
            });

            modelBuilder.Entity<SalaryReport>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__SalaryRe__D5BD48E5BED48564");

                entity.ToTable("SalaryReport");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.Period).HasColumnType("date");

                entity.Property(e => e.Total).HasComputedColumnSql("([BaseSalary]+[TotalBonus])", false);

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.SalaryReports)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalaryReport_Employee");
            });

            modelBuilder.Entity<ShiftConfig>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ShiftConfig");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.HasOne(d => d.Position)
                    .WithMany()
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShiftConfig_Position");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveriedTime).HasColumnType("datetime");

                entity.Property(e => e.EstimateShippedTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ShippingTime).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipment_Order");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.BrandToApplyNavigation)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.BrandToApply)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_Brand");

                entity.HasOne(d => d.CategoryToApplyNavigation)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.CategoryToApply)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_Category");

                entity.HasOne(d => d.ProductToApplyNavigation)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.ProductToApply)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_Product");
            });

            modelBuilder.Entity<VoucherUsed>(entity =>
            {
                entity.HasKey(e => e.UsedId)
                    .HasName("PK__VoucherU__0BF8B8315EC07971");

                entity.ToTable("VoucherUsed");

                entity.Property(e => e.UsedId).HasColumnName("UsedID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UsedDate).HasColumnType("datetime");

                entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.VoucherUseds)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherUsed_Customer");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.VoucherUseds)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherUsed_Order");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.VoucherUseds)
                    .HasForeignKey(d => d.VoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherUsed_Voucher");
            });

            modelBuilder.Entity<WorkShift>(entity =>
            {
                entity.ToTable("WorkShift");

                entity.Property(e => e.WorkShiftId).HasColumnName("WorkShiftID");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.SalaryIndex).HasComputedColumnSql("([dbo].[FN_WorkShift_SalaryIndex]([WorkShiftID]))", false);

                entity.Property(e => e.Status).HasComputedColumnSql("([dbo].[FN_WorkShift_Status]([WorkShiftID]))", false);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.WorkShifts)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkShift_Position");

                entity.HasOne(d => d.Worksheet)
                    .WithMany(p => p.WorkShifts)
                    .HasPrincipalKey(p => new { p.Date, p.PositionId })
                    .HasForeignKey(d => new { d.Date, d.PositionId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkShift_Worksheet");
            });

            modelBuilder.Entity<WorkShiftEmployee>(entity =>
            {
                entity.HasKey(e => e.WsempId)
                    .HasName("PK__WorkShif__46F00319E3FF2C0A");

                entity.ToTable("WorkShift_Employee");

                entity.Property(e => e.WsempId).HasColumnName("WSEmpID");

                entity.Property(e => e.CheckinTime).HasColumnType("datetime");

                entity.Property(e => e.CheckoutTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasComputedColumnSql("([dbo].[FN_WSE_Date]([WSEmpID]))", false);

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasComputedColumnSql("([dbo].[FN_WSE_Status]([EmpID]))", false);

                entity.Property(e => e.WorkShiftId).HasColumnName("WorkShiftID");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.WorkShiftEmployees)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkShift_Employee_Employee");

                entity.HasOne(d => d.WorkShift)
                    .WithMany(p => p.WorkShiftEmployees)
                    .HasForeignKey(d => d.WorkShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkShift_Employee_WorkShift");
            });

            modelBuilder.Entity<Worksheet>(entity =>
            {
                entity.ToTable("Worksheet");

                entity.HasIndex(e => new { e.Date, e.PositionId }, "CK_Worksheet_Date")
                    .IsUnique();

                entity.Property(e => e.WorksheetId).HasColumnName("WorksheetID");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.SalaryIndex).HasComputedColumnSql("([dbo].[FN_WorkSheet_SalaryIndex]([WorksheetID]))", false);

                entity.Property(e => e.Status).HasComputedColumnSql("([dbo].[FN_Worksheet_Status]([Date],[PositionID]))", false);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Worksheets)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Worksheet_Position");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
