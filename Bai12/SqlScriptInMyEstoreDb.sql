USE MyeStore

SELECT MaHH, TenHH, TenLoai, TenCongTy as NhaCungCap
FROM vHangHoa
WHERE DonGia BETWEEN 100 AND 1000

--Tạo view lấy thông tin hóa đơn
CREATE VIEW vHoaDon
AS
SELECT cthd.*, TenHH, TenLoai, TenCongTy as NhaCungCap
FROM ChiTietHD cthd JOIN vHangHoa hh 
		ON hh.MaHH = cthd.MaHH

--Sử dụng View
SELECT * FROM vHoaDon WHERE MaHD = 10248


-----------------------STORE PROCEDURE
--Viết store thống kê doanh thu hàng hóa bán được
--theo ngày tháng, loại và nhà cung cấp
CREATE PROC spThongKeDoanhThu	
	@Thang int, @Nam int
AS
BEGIN
	SELECT TenLoai, TenHH, 
		SUM(cthd.SoLuong * cthd.DonGia) as DoanhThu
	FROM HoaDon hd JOIN ChiTietHD cthd ON hd.MaHD = cthd.MaHD
		JOIN HangHoa hh ON cthd.MaHH = hh.MaHH
		JOIN Loai lo ON lo.MaLoai = hh.MaLoai
	WHERE YEAR(NgayDat) = @Nam AND MONTH(NgayDat) = @Thang
	GROUP BY hh.MaHH, TenHH, TenLoai
END

EXEC spThongKeDoanhThu 4, 1997


---Viết store thêm mới loại
CREATE PROC spThemLoai
	@MaLoai int output, --lấy giá trị ra (sau khi chạy)
	@TenLoai nvarchar(50),
	@Hinh nvarchar(50),
	@MoTa nvarchar(max)
AS BEGIN
	--Thêm mới
	INSERT INTO Loai(TenLoai, MoTa, Hinh)
		VALUES(@TenLoai, @MoTa, @Hinh)
	--Lấy giá trị mã loại vừa sinh
	SELECT @MaLoai = @@IDENTITY
END

--demo gọi trong SQL
DECLARE @Ma int
EXEC spThemLoai @Ma OUTPUT, N'Bánh đa', null, 
					N'Các loại bánh đa'
PRINT CONCAT(N'Vừa thêm loại có mã : ', @Ma)

--Lấy tất cả (có filter theo TuKhoa)
CREATE PROC spLayTatCaLoai
	@TuKhoa nvarchar(50)
AS BEGIN
	SELECT * FROM Loai 
	WHERE TenLoai LIKE N'%' + @TuKhoa + N'%'
		OR MoTa LIKE N'%' + @TuKhoa + N'%'
	ORDER BY TenLoai
END

--Lấy loại theo tên
CREATE PROC spLayLoai
	@MaLoai int
AS BEGIN
	SELECT * FROM Loai  WHERE MaLoai = @MaLoai
END

--Sửa
CREATE PROC spSuaLoai
	@MaLoai int,
	@TenLoai nvarchar(50),
	@Hinh nvarchar(50),
	@MoTa nvarchar(max)
AS BEGIN
	UPDATE Loai  SEt TenLoai = @TenLoai, MoTa = @MoTa,
		Hinh = @Hinh WHERE MaLoai = @MaLoai
END

--xóa
CREATE PROC spXoaLoai
	@MaLoai int
AS BEGIN
	DELETE FROM Loai  WHERE MaLoai = @MaLoai
END

--Lấy tất cả hàng hóa (có filter theo TuKhoa)
CREATE PROC spLayTatCaHangHoa
	@TuKhoa nvarchar(50)
AS BEGIN
	SELECT * FROM HangHoa 
	WHERE TenHH LIKE N'%' + @TuKhoa + N'%'
		OR MoTa LIKE N'%' + @TuKhoa + N'%'
	ORDER BY TenHH
END

---FUNCTION
--Viết hàm tính doanh thu theo khách hàng
CREATE FUNCTION fDoanhThuTheoKhachHang
(
	@MaKH nvarchar(50), @Nam int, @Thang int
)
RETURNS FLOAT
AS BEGIN
	--B1. Khai báo biến giữ kết quả trả về
	DECLARE @DoanhSo float
	--B2: Tính toán
	SELECT @DoanhSo = SUM(SoLuong * DonGia * (1 - GiamGia))
	FROM HoaDon hd JOIN ChiTietHD cthd 
		ON hd.MaHD = cthd.MaHD
	WHERE MaKH = @MaKH AND YEAR(NgayDat) = @Nam
		AND MONTH(NgayDat) = @Thang
	--B3: Trả về
	RETURN @DoanhSo
END

--Demo
SELECT dbo.fDoanhThuTheoKhachHang('ANTON', 1997, 4)
SELECT MaKH, HoTen,dbo.fDoanhThuTheoKhachHang(MaKH, 1997, 4)
FROM KhachHang

--Function trả về dạng bảng
CREATE FUNCTION fThongKeHangHoaTheoNam( @Nam int)
RETURNS TABLE
AS
RETURN
	SELECT TenHH, SUM(cthd.SoLuong * cthd.DonGia) as DoanhSo
	FROM HangHoa hh JOIN ChiTietHD cthd ON
		hh.MaHH = cthd.MaHH
		JOIN HoaDon hd ON hd.MaHD = cthd.MaHD
	WHERE YEAR(NgayDat) = @Nam
	GROUP BY TenHH

--demo
SELECT * FROM dbo.fThongKeHangHoaTheoNam(1997)

--VD function trả về table (định nghĩa các cột sẵn)
CREATE FUNCTION fThongKeDonHang()
RETURNS @MyTable TABLE(
		Nam int, SoLuong int, DoanhSo float
	)
AS BEGIN
	INSERT INTO @MyTable
	SELECT YEAR(NgayDat) Nam, COUNT(cthd.MaCT) SoLuong,
		SUM(SoLuong * DonGia * (1 - GiamGia)) DoanhSo		
	FROM HoaDon hd JOIn ChiTietHD cthd 
		ON hd.MaHD = cthd.MaHD
	GROUP BY YEAR(NgayDat)
	RETURN
END

--TRIGGER
--tự động update cột tổng tiền của hóa đơn khi thêm/xóa/sửa CTHD
CREATE TRIGGER trgCapNhatThanhTien	ON ChiTietHD
	AFTER INSERT, UPDATE, DELETE
AS BEGIN
	DECLARE @MaHD int;
	DECLARE @Tong float;
	
	/*
	WITH BangTam AS(
		SELECT MaHD FROM inserted
		UNION
		SELECT MAHD FROM deleted
	)

	SELECT @MaHD = MaHD FROM BangTam
	*/
	SELECT @MaHD = MaHD FROM
	(
		SELECT MaHD FROM inserted
		UNION
		SELECT MAHD FROM deleted
	) as tmp

	SELECT @Tong = SUM(SoLuong * DonGia * (1 - GiamGia))
	FROM ChiTietHD WHERE MaHD = @MaHD

	UPDATE HoaDon SET TongTien = @Tong WHERE MaHD = @MaHD
END