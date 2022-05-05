drop database QuanLyThuVien
create database QuanLyThuVien
use QuanLyThuVien


create table DocGia
(
	IDDG int primary key Identity(1,1),
	TenDG nvarchar(50) ,
	DienThoai nvarchar(50) ,
	DiaChi nvarchar(50) ,
	UserName nvarchar(50)  , 
	Password nvarchar(50)  ,
)

go
create table PhieuMuon
(
	IDPM int primary key identity(1,1),
	IDDG int,
	TenDG nvarchar(50)  ,
	NgayMuon datetime  ,
	NgayTra datetime  ,
	TienPhat int ,
	GhiChu nvarchar(50) ,
	TrangThai nvarchar(50),
	CONSTRAINT FK_PHIEUMUON_DOCGIA FOREIGN KEY (IDDG) REFERENCES DocGia(IDDG)
)

create table TheLoai
(
	IDCate int primary key Identity(1,1),
	NameCate NVARCHAR(50)  ,
)

insert into TheLoai(NameCate)
values (N'Chính trị - pháp luật')
insert into TheLoai(NameCate)
values (N'Khoa học công nghệ - kinh tế')
insert into TheLoai(NameCate)
values (N'Văn học nghệ thuật')
insert into TheLoai(NameCate)
values (N'Văn hóa xã hội – Lịch sử')
insert into TheLoai(NameCate)
values (N'Giáo trình')
insert into TheLoai(NameCate)
values (N'Truyện, tiểu thuyết')
insert into TheLoai(NameCate)
values (N'Tâm lý, tâm linh, tôn giáo')
insert into TheLoai(NameCate)
values (N'Sách thiếu nhi')
insert into TheLoai(NameCate)
values (N'Self help')

SELECT * FROM TheLoai
 
create table Sach
(
	IDSach nvarchar(10) primary key,
	TenSach NVARCHAR(50)  ,
	TheLoai int,
	MoTa NVARCHAR(max)  ,
	TacGia NVARCHAR(50)  ,
	NgayXuatBan datetime  ,
	SoLuong int ,
	HinhAnh NVARCHAR(max) ,
	CONSTRAINT FK_SACH_THELOAI FOREIGN KEY (TheLoai) REFERENCES TheLoai(IDCate)
)
go

Create trigger TaoIDSach
on Sach
INSTEAD OF INSERT
as 
begin 
	
	DECLARE @idSach NVARCHAR(10), @tensach nvarchar(50), @theloai nvarchar(50), @MoTa NVARCHAR(50),@TacGia NVARCHAR(50),@NgayXuatBan datetime
	DECLARE @SoLuong int, @HinhAnh NVARCHAR(max)	
	select @tensach = TenSach, @MoTa =MoTa, @TacGia = TacGia, @HinhAnh = HinhAnh from inserted
	select @theloai = TheLoai,@NgayXuatBan = NgayXuatBan, @SoLuong = SoLuong from inserted	
    declare @id int
	set @id = 1
	set @idSach = LEFT(@tensach,3) + LEFT(@theloai,3) + CAST(@id AS NVARCHAR(10))
	while ((select Count(*) from Sach where IDSach = @idSach) != 0 )
	BEGIN    
		SET @id = @id  + 1
		set @idSach = LEFT(@tensach,3) + LEFT(@theloai,3) + CAST(@id AS NVARCHAR(10))		
	END
	
	insert into Sach(IDSach,TenSach,TheLoai,MoTa,TacGia,NgayXuatBan,SoLuong,HinhAnh)
	values(@idSach,@tensach,@theloai,@MoTa,@TacGia,@NgayXuatBan,@SoLuong,@HinhAnh)

	EXEC ThemTuDongChiTietSach @soluong = @SoLuong, @idSach = @idSach
end

go



create table ChiTietSach
(
	IDChiTietSach nvarchar(20) primary key,
	IDSach nvarchar(10),
	TinhTrang nvarchar(50),
	CONSTRAINT FK_ChiTietSach_Sach FOREIGN KEY (IDSach) REFERENCES SACH(IDSach)
)
go

create PROCEDURE ThemTuDongChiTietSach @soluong nvarchar(30), @idSach nvarchar(10)
AS
declare @id int
set @id = @soluong
while (@id > 0) 
begin
	declare @newID nvarchar(20)
	set @newID = @idSach + CAST(@id AS NVARCHAR(10))
	insert into ChiTietSach(IDChiTietSach,IDSach,TinhTrang)
	values(@newID,@idSach,N'Đang dùng');
	set @id = @id -1
end

create table CT_PM
(
	ID int Identity(1,1) primary key ,
	IDPM int ,
	IDDG int ,
	TenDG nvarchar(50)  ,
	IDSach nvarchar(10) ,
	TenSach nvarchar(50) ,
	SoLuong int ,
	TrangThai nvarchar(50) ,
	NgayTraThucTe datetime ,

	CONSTRAINT FK_CT_PM_DocGia FOREIGN KEY (IDDG) REFERENCES DocGia(IDDG),
	CONSTRAINT FK_CT_PM_PhieuMuon FOREIGN KEY (IDPM) REFERENCES PhieuMuon(IDPM),
	CONSTRAINT FK_CT_PM_Sach FOREIGN KEY (IDSach) REFERENCES Sach(IDSach),
)
go

create table DangNhap
(
	ID int primary key Identity(1,1),
	UserName nvarchar(50),
	Password nvarchar(50)  ,
)

insert into DangNhap (UserName, Password)
values('123','123')

delete ChiTietSach
delete Sach
select * from Sach

create trigger TuDongSuaDoiTrangThaiMuonSachKhiTraHoanThanh
on CT_PM
after update 
as 
begin 
	DECLARE @IDPM NVARCHAR(10), @SoSachChuaTra int
	select @IDPM = IDPM from inserted
	select @SoSachChuaTra = count(id) from CT_PM where IDPM = @IDPM and TrangThai = N'Đang mượn'

   if(@SoSachChuaTra = 0)
   begin 
		update PhieuMuon set TrangThai = N'Đã trả' where IDPM = @IDPM
   end 	
end

