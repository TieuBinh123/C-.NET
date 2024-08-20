Create database QuanLyThuVien
use QuanLyThuVien
Go

--Tham Gia
CREATE TABLE [dbo].[ThamGia](
	[MaSach] [nvarchar](10) NOT NULL,
	[MaTacGia] [nvarchar](10) NOT NULL,
	[VaiTro] [nvarchar](50) NULL,
	[ViTri] [nvarchar](50) NULL,
 CONSTRAINT [PK_ThamGia] PRIMARY KEY CLUSTERED 
(
	[MaSach] ASC,
	[MaTacGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


--Chi Tiet Phieu Muon

CREATE TABLE [dbo].[ChiTietPhieuMuon](
	[MaPhieuMuon] [nvarchar](10) NOT NULL,
	[MaSach] [nvarchar](10) NOT NULL,
	[SoLuong] [int] NULL,
	[DonGia] [decimal](18, 0) NULL,
 CONSTRAINT [PK_ChiTietPhieuMuon] PRIMARY KEY CLUSTERED 
(
	[MaPhieuMuon] ASC,
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




--Tac Gia

CREATE TABLE [dbo].[TacGia](
	[MaTacGia] [nvarchar](10) NOT NULL,
	[TenTacGia] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[TieuSu] [nvarchar](max) NULL,
	[DienThoai] [nvarchar](50) NULL,
 CONSTRAINT [PK_TacGia] PRIMARY KEY CLUSTERED 
(
	[MaTacGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


--Sach

CREATE TABLE [dbo].[Sach](
	[MaSach] [nvarchar](10) NOT NULL,
	[TenSach] [nvarchar](100) NULL,
	[GiaTien] [decimal](18, 0) NULL,
	[MoTa] [nvarchar](max) NULL,
	[NgayXuatBan] [datetime] NULL,
	[SoLuongTon] [int] NULL,
	[MaChuDe] [nvarchar](10) NULL,
	[MaNXB] [nvarchar](10) NULL,
	--[Moi] [int] NULL,
 CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED 
(
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



--NhaXuatBan

CREATE TABLE [dbo].[NhaXuatBan](
	[MaNXB] [nvarchar](10) NOT NULL,
	[TenNXB] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [varchar](50) NULL,
 CONSTRAINT [PK_NhaXuatBan] PRIMARY KEY CLUSTERED 
(
	[MaNXB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--User

CREATE TABLE [dbo].[User](
	[MaUser] [nvarchar](10) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[NgaySinh] [datetime] NULL,
	[GioiTinh] [nvarchar](3) NULL,
	[DienThoai] [varchar](50) NULL,
	[TaiKhoan] [varchar](50) NULL,
	[MatKhau] [nvarchar](50) NULL,
	[Email] [varchar](50) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[PhanLoai] [nvarchar](30) NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--drop table [dbo].[User]
--Phieu Muon

CREATE TABLE [dbo].[PhieuMuon](
	[MaPhieuMuon] [nvarchar](10) NOT NULL,
	[NgayMuon] [datetime] NULL,
	[PhiPhat] int NULL,
	[TinhTrang] [nvarchar](100) NULL,
	[MaUser] [nvarchar](10) NULL,
	[LDTC] [nvarchar](50) NULL,
 CONSTRAINT [PK_PhieuMuon] PRIMARY KEY CLUSTERED 
(
	[MaPhieuMuon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
Go
--drop table dbo.PhieuMuon


-- Xóa khóa ngoại FK_PhieuMuon_User trên bảng PhieuMuon
--ALTER TABLE PhieuMuon DROP CONSTRAINT FK_PhieuMuon_User;

-- Xóa khóa ngoại FK_User_PhieuMuon trên bảng User
--ALTER TABLE dbo.[User] DROP CONSTRAINT FK_User_PhieuMuon;


--Chu De

CREATE TABLE [dbo].[ChuDe](
	[MaChuDe] [nvarchar](10) NOT NULL,
	[TenChuDe] [nvarchar](50) NULL,
 CONSTRAINT [PK_ChuDe] PRIMARY KEY CLUSTERED 
(
	[MaChuDe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
--Khoa ngoai
--Tham Gia
ALTER TABLE [dbo].[ThamGia]  WITH CHECK ADD  CONSTRAINT [FK_ThamGia_Sach] FOREIGN KEY([MaSach])
REFERENCES [dbo].[Sach] ([MaSach])
GO

ALTER TABLE [dbo].[ThamGia] CHECK CONSTRAINT [FK_ThamGia_Sach]
GO

ALTER TABLE [dbo].[ThamGia]  WITH CHECK ADD  CONSTRAINT [FK_ThamGia_TacGia] FOREIGN KEY([MaTacGia])
REFERENCES [dbo].[TacGia] ([MaTacGia])
GO

ALTER TABLE [dbo].[ThamGia] CHECK CONSTRAINT [FK_ThamGia_TacGia]
GO

--Chi tiết Phiếu Mượn
ALTER TABLE [dbo].[ChiTietPhieuMuon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuMuon_PhieuMuon] FOREIGN KEY([MaPhieuMuon])
REFERENCES [dbo].[PhieuMuon] ([MaPhieuMuon])
GO

ALTER TABLE [dbo].[ChiTietPhieuMuon] CHECK CONSTRAINT [FK_ChiTietPhieuMuon_PhieuMuon]
GO

ALTER TABLE [dbo].[ChiTietPhieuMuon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuMuon_Sach] FOREIGN KEY([MaSach])
REFERENCES [dbo].[Sach] ([MaSach])
GO

ALTER TABLE [dbo].[ChiTietPhieuMuon] CHECK CONSTRAINT [FK_ChiTietPhieuMuon_Sach]
GO
--Sach
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_ChuDe] FOREIGN KEY([MaChuDe])
REFERENCES [dbo].[ChuDe] ([MaChuDe])
GO

ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_ChuDe]
GO

ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_NhaXuatBan] FOREIGN KEY([MaNXB])
REFERENCES [dbo].[NhaXuatBan] ([MaNXB])
GO

ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_NhaXuatBan]
GO
--**
ALTER TABLE [dbo].[PhieuMuon]  WITH CHECK ADD  CONSTRAINT [FK_PhieuMuon_User] FOREIGN KEY([MaUser])
REFERENCES [dbo].[User] ([MaUser])
GO

ALTER TABLE [dbo].[PhieuMuon] CHECK CONSTRAINT [FK_PhieuMuon_User]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- Dữ Liệu 

INSERT INTO dbo.TacGia (MaTacGia,TenTacGia, DiaChi, TieuSu, DienThoai)
VALUES
('TG1',N'Nguyễn Nhật Ánh', N'Số 123 Nguyễn Trãi, Thanh Xuân, Hà Nội', N'Nhà văn chuyên viết truyện thiếu nhi', '0243.856.2160'),
('TG2',N'Võ Thị Hảo', N'Số 456 Hai Bà Trưng, Hoàn Kiếm, Hà Nội', N'Nhà văn chuyên viết truyện ngắn', '0243.937.3051'),
('TG3',N'Nguyễn Huy Thiệp', N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội', N'Nhà văn chuyên viết tiểu thuyết', '0243.825.2846'),
('TG4',N'Lê Lựu', N'Số 1011 Kim Mã, Ba Đình, Hà Nội', N'Nhà văn chuyên viết truyện dài', '0243.936.2152'),
('TG5',N'Nam Cao', N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội', N'Nhà văn chuyên viết truyện ngắn', '0243.824.3163'),
('TG6',N'Lỗ Tấn', N'Số 1315 Nguyễn Xiển, Thanh Xuân, Hà Nội', N'Nhà văn chuyên viết tiểu thuyết', '0243.823.4174'),
('TG7',N'Mao Thuẫn', N'Số 1417 Trần Duy Hưng, Cầu Giấy, Hà Nội', N'Nhà văn chuyên viết kịch', '0243.822.5185'),
('TG8',N'Lưu Hiểu Ba', N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội', N'Nhà văn chuyên viết tiểu luận', '0243.821.6196'),
('TG9',N'Tenzin Gyatso - Đức Đạt Lai Lạt Ma 14',N'Số 123 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Tenzin Gyatso, nói đủ là Jetsun Jamphel Ngawang Lobsang Yeshe Tenzin Gyatso, là pháp hiệu của Đức Đạt Lai Lạt Ma thứ 14 là vị đạo sư lãnh đạo tinh thần của người dân Tây Tạng và nhiều đạo tràng Phật Giáo trên thế giới. Đức Đạt Lai Lạt Ma là tước hiệu của vua Mông Cổ Altan Khan ban cho Lạt ma Sonam Gyatso vào năm 1578. Từ đó, “Đức Đạt Lai Lạt Ma” trở thành danh xưng cho vị Lạt ma cao nhất trong truyền thống Phật giáo Gelug (Mũ Vàng). Đạt Lai Lạt Ma (Dalai Lama) nghĩa là "Người bảo vệ đức tin" (Defender of the Faith), "Biển lớn của trí tuệ" (Ocean of Wisdom), "Vua của Chánh Pháp" (King of Dharma), “Viên bảo châu như ý” (Wishfulfilling Gem), “Hoa sen trắng” (White lotus) và Hóa thân Quan Âm (Kuan Yin Boddhisattva).','0243.856.2160'),
('TG10',N'Trịnh Xuân Thuận',N'Số 456 Hai Bà Trưng, Hoàn Kiếm, Hà Nội',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','0243.937.3051'),
('TG11',N'Matthieu Ricard',N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG12',N'Kyabje Khamtrul Rinpoche',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG13',N'Đức Pháp Vương GYALWANG DRUKPA XII',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG14',N'Nguyên Phong',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG15',N'Lama Zopa Rinpoche',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG16',N'Lama Anagarika Govinda',N'Số 1315 Nguyễn Xiển, Thanh Xuân, Hà Nội',N'Tenzin Gyatso, nói đủ là Jetsun Jamphel Ngawang Lobsang Yeshe Tenzin Gyatso, là pháp hiệu của Đức Đạt Lai Lạt Ma thứ 14 là vị đạo sư lãnh đạo tinh thần của người dân Tây Tạng và nhiều đạo tràng Phật Giáo trên thế giới. Đức Đạt Lai Lạt Ma là tước hiệu của vua Mông Cổ Altan Khan ban cho Lạt ma Sonam Gyatso vào năm 1578. Từ đó, “Đức Đạt Lai Lạt Ma” trở thành danh xưng cho vị Lạt ma cao nhất trong truyền thống Phật giáo Gelug (Mũ Vàng). Đạt Lai Lạt Ma (Dalai Lama) nghĩa là "Người bảo vệ đức tin" (Defender of the Faith), "Biển lớn của trí tuệ" (Ocean of Wisdom), "Vua của Chánh Pháp" (King of Dharma), “Viên bảo châu như ý” (Wishfulfilling Gem), “Hoa sen trắng” (White lotus) và Hóa thân Quan Âm (Kuan Yin Boddhisattva).','1234'),
('TG17',N'Ni sư Thích Nữ Trí Hải',N'Số 1315 Nguyễn Xiển, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG18',N'Ngọc Bích',N'Số 1315 Nguyễn Xiển, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG19',N'Quỳnh Nga',N'Số 1315 Nguyễn Xiển, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG20',N'Edward Peppitt',N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội',N'Kyabje-Khamtrul-Rinpoche','1234'),
('TG21',N'Nhân Văn',N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội',N'Tenzin Gyatso, nói đủ là Jetsun Jamphel Ngawang Lobsang Yeshe Tenzin Gyatso, là pháp hiệu của Đức Đạt Lai Lạt Ma thứ 14 là vị đạo sư lãnh đạo tinh thần của người dân Tây Tạng và nhiều đạo tràng Phật Giáo trên thế giới. Đức Đạt Lai Lạt Ma là tước hiệu của vua Mông Cổ Altan Khan ban cho Lạt ma Sonam Gyatso vào năm 1578. Từ đó, “Đức Đạt Lai Lạt Ma” trở thành danh xưng cho vị Lạt ma cao nhất trong truyền thống Phật giáo Gelug (Mũ Vàng). Đạt Lai Lạt Ma (Dalai Lama) nghĩa là "Người bảo vệ đức tin" (Defender of the Faith), "Biển lớn của trí tuệ" (Ocean of Wisdom), "Vua của Chánh Pháp" (King of Dharma), “Viên bảo châu như ý” (Wishfulfilling Gem), “Hoa sen trắng” (White lotus) và Hóa thân Quan Âm (Kuan Yin Boddhisattva).','1234'),
('TG22',N'Blair T.Spalding',N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội',N'Phó viện trưởng viện Nghiên cứu Phật học Việt Nam','1234'),
('TG23',N'FPT Polytechnic',N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội',N'Phó viện trưởng viện Nghiên cứu Phật học Việt Nam','1234'),
('TG24',N'Upasika Kee Nanayon',N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội',N'Matthieu-Ricard','1234'),
('TG25',N'Inamori Kazuo',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Matthieu-Ricard','1234'),
('TG26',N'Robin Lewis',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Phó viện trưởng viện Nghiên cứu Phật học Việt Nam','1234'),
('TG27',N'Michael Dart',N'Số 1011 Kim Mã, Ba Đình, Hà Nội',N'Tenzin Gyatso, nói đủ là Jetsun Jamphel Ngawang Lobsang Yeshe Tenzin Gyatso, là pháp hiệu của Đức Đạt Lai Lạt Ma thứ 14 là vị đạo sư lãnh đạo tinh thần của người dân Tây Tạng và nhiều đạo tràng Phật Giáo trên thế giới. Đức Đạt Lai Lạt Ma là tước hiệu của vua Mông Cổ Altan Khan ban cho Lạt ma Sonam Gyatso vào năm 1578. Từ đó, “Đức Đạt Lai Lạt Ma” trở thành danh xưng cho vị Lạt ma cao nhất trong truyền thống Phật giáo Gelug (Mũ Vàng). Đạt Lai Lạt Ma (Dalai Lama) nghĩa là "Người bảo vệ đức tin" (Defender of the Faith), "Biển lớn của trí tuệ" (Ocean of Wisdom), "Vua của Chánh Pháp" (King of Dharma), “Viên bảo châu như ý” (Wishfulfilling Gem), “Hoa sen trắng” (White lotus) và Hóa thân Quan Âm (Kuan Yin Boddhisattva).','1234'),
('TG28',N'Mary T.Browne',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Tenzin Gyatso, nói đủ là Jetsun Jamphel Ngawang Lobsang Yeshe Tenzin Gyatso, là pháp hiệu của Đức Đạt Lai Lạt Ma thứ 14 là vị đạo sư lãnh đạo tinh thần của người dân Tây Tạng và nhiều đạo tràng Phật Giáo trên thế giới. Đức Đạt Lai Lạt Ma là tước hiệu của vua Mông Cổ Altan Khan ban cho Lạt ma Sonam Gyatso vào năm 1578. Từ đó, “Đức Đạt Lai Lạt Ma” trở thành danh xưng cho vị Lạt ma cao nhất trong truyền thống Phật giáo Gelug (Mũ Vàng). Đạt Lai Lạt Ma (Dalai Lama) nghĩa là "Người bảo vệ đức tin" (Defender of the Faith), "Biển lớn của trí tuệ" (Ocean of Wisdom), "Vua của Chánh Pháp" (King of Dharma), “Viên bảo châu như ý” (Wishfulfilling Gem), “Hoa sen trắng” (White lotus) và Hóa thân Quan Âm (Kuan Yin Boddhisattva).','1234'),
('TG29',N'Minh Đức',N'Số 1011 Kim Mã, Ba Đình, Hà Nội',N'Tenzin Gyatso, nói đủ là Jetsun Jamphel Ngawang Lobsang Yeshe Tenzin Gyatso, là pháp hiệu của Đức Đạt Lai Lạt Ma thứ 14 là vị đạo sư lãnh đạo tinh thần của người dân Tây Tạng và nhiều đạo tràng Phật Giáo trên thế giới. Đức Đạt Lai Lạt Ma là tước hiệu của vua Mông Cổ Altan Khan ban cho Lạt ma Sonam Gyatso vào năm 1578. Từ đó, “Đức Đạt Lai Lạt Ma” trở thành danh xưng cho vị Lạt ma cao nhất trong truyền thống Phật giáo Gelug (Mũ Vàng). Đạt Lai Lạt Ma (Dalai Lama) nghĩa là "Người bảo vệ đức tin" (Defender of the Faith), "Biển lớn của trí tuệ" (Ocean of Wisdom), "Vua của Chánh Pháp" (King of Dharma), “Viên bảo châu như ý” (Wishfulfilling Gem), “Hoa sen trắng” (White lotus) và Hóa thân Quan Âm (Kuan Yin Boddhisattva).','1234'),
('TG30',N'John C.Maxwell',N'Số 1011 Kim Mã, Ba Đình, Hà Nội',N'Tenzin Gyatso, nói đủ là Jetsun Jamphel Ngawang Lobsang Yeshe Tenzin Gyatso, là pháp hiệu của Đức Đạt Lai Lạt Ma thứ 14 là vị đạo sư lãnh đạo tinh thần của người dân Tây Tạng và nhiều đạo tràng Phật Giáo trên thế giới. Đức Đạt Lai Lạt Ma là tước hiệu của vua Mông Cổ Altan Khan ban cho Lạt ma Sonam Gyatso vào năm 1578. Từ đó, “Đức Đạt Lai Lạt Ma” trở thành danh xưng cho vị Lạt ma cao nhất trong truyền thống Phật giáo Gelug (Mũ Vàng). Đạt Lai Lạt Ma (Dalai Lama) nghĩa là "Người bảo vệ đức tin" (Defender of the Faith), "Biển lớn của trí tuệ" (Ocean of Wisdom), "Vua của Chánh Pháp" (King of Dharma), “Viên bảo châu như ý” (Wishfulfilling Gem), “Hoa sen trắng” (White lotus) và Hóa thân Quan Âm (Kuan Yin Boddhisattva).','1234'),
('TG31',N'Dr. Lin Lougheed, Ed.D',N'',N'Phó viện trưởng viện Nghiên cứu Phật học Việt Nam','1234'),
('TG32',N'Đổ Thái Hòa',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG33',N'Barbara Bazaldua',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG34',N'Th.S Nguyễn Nam Thuận',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Phó viện trưởng viện Nghiên cứu Phật học Việt Nam',''),
('TG35',N'Lê Thuận',N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội',N'Matthieu-Ricard','1234'),
('TG36',N'Trần Tuấn Mẫn',N'Số 1519 Nguyễn Trãi, Thanh Xuân, Hà Nội',N'Matthieu-Ricard','1234'),
('TG37',N'Dr. Prashant Kakode',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Matthieu-Ricard','1234'),
('TG38',N'John Blofeld',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG39',N'Nguyễn Bá Tiến',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Matthieu-Ricard','1234'),
('TG40',N'Vô Úy',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Matthieu-Ricard','1234'),
('TG41',N'Quang Hiển',N'179 Chánh Hưng F.4 Q.8 Tp.HCM',N'Matthieu-Ricard','1234'),
('TG42',N'Tường Thụy',N'179 Chánh Hưng F.4 Q.8 Tp.HCM',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG43',N'Huyền Cơ',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG44',N'Tony Buzan',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG45',N'Dale Carnegie ',N'Số 789 Nguyễn Chí Thanh, Ba Đình, Hà Nội',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG46',N'Stephen P. Robbins .Timothy A. Judge',N'Số 1213 Giải Phóng, Thanh Xuân, Hà Nội',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG47',N'Vương Chí Cương',N'179 Chánh Hưng F.4 Q.8 Tp.HCM',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234'),
('TG48',N'James Chen. Lê Đạt Chí',N'179 Chánh Hưng F.4 Q.8 Tp.HCM',N'Trịnh Xuân Thuận (1948- ) là một khoa học gia người Mỹ gốc Việt trong lĩnh vực vật lý thiên văn, là một nhà văn đã viết nhiều quyển sách có giá trị cao về vũ trụ học và về những suy nghĩ của ông trong tương quan giữa khoa học và Phật giáo. Ông còn là một nhà thơ, một triết gia, một Phật tử và một nhà hoạt động cho môi trường và hòa bình. Ông đã nhận lãnh nhiều giải thưởng trong lĩnh vực thiên văn và trong lĩnh vực văn hoá xã hội.','1234')

go
INSERT INTO dbo.NhaXuatBan (MaNXB ,TenNXB, DiaChi, DienThoai)
VALUES
('NXB1',N'Nhà xuất bản Giáo dục Việt Nam', N'Số 49 Chùa Láng, Láng Thượng, Đống Đa, Hà Nội', '0243.568.6901'),
('NXB2',N'Nhà xuất bản Kim Đồng', N'Số 50 Lê Văn Lương, Nhân Chính, Thanh Xuân, Hà Nội', '0243.557.1894'),
('NXB3',N'Nhà xuất bản Trẻ', N'Số 65-67 Võ Văn Tần, Phường 6, Quận 3, TP. Hồ Chí Minh', '0283.822.2615'),
('NXB4',N'Nhà xuất bản Hải Phòng', N'Số 51 Lạch Tray, Ngô Quyền, Hải Phòng', '0225.823.5831'),
('NXB5',N'Nhà xuất bản Đà Nẵng', N'Số 14 Trần Quốc Toản, Quận Hải Châu, Đà Nẵng', '0236.382.7452'),
('NXB6',N'Nhà xuất bản Văn học', N'Số 34 Hàng Chuối, Hoàn Kiếm, Hà Nội', '0243.825.2846'),
('NXB7',N'Nhà xuất bản Thế giới', N'Số 46 Trần Hưng Đạo, Quận Hoàn Kiếm, Hà Nội', '0243.937.3051'),
('NXB8',N'Nhà xuất bản Thanh Niên', N'Số 218 Lý Thường Kiệt, Quận 11, TP. Hồ Chí Minh', '0283.856.2160'),
('NXB9',N'Nhà xuất bản Phụ nữ', N'Số 10 Lý Thường Kiệt, Quận Hoàn Kiếm, Hà Nội', '0243.825.2846'),
('NXB10',N'Nhà xuất bản Tri Thức', N'Số 1 Đinh Lễ, Hoàn Kiếm, Hà Nội', '0243.825.2846')
go
INSERT INTO dbo.ChuDe (MaChuDe,TenChuDe)
VALUES
('CD1',N'Công nghệ thông tin'),
('CD2',N'Ngoại ngữ'),
('CD3',N'Phật Giáo'),
('CD4',N'Nghệ thuật'),
('CD5',N'Thiếu nhi'),
('CD6',N'Truyện tranh'),
('CD7',N'Kinh Tế'),
('CD8',N'Khoa học Vật lý'),
('CD9',N'Khoa học Xã hội'),
('CD10',N'Luật'),
('CD11',N'Từ điển'),
('CD12',N'Lịch sử, địa lý'),
('CD13',N'Vật nuôi - Cây cảnh'),
('CD14',N'Khoa học kỹ thuật'),
('CD15',N'Mỹ thuật'),
('CD16',N'Nghệ thuật'),
('CD17',N'Âm nhạc'),
('CD18',N'Sách giáo khoa'),
('CD19',N'Sách tham khảo'),
('CD20',N'Danh nhân'),
('CD21',N'Tâm lý giáo dục'),
('CD22',N'Thể thao - Võ thuật'),
('CD23',N'Văn hóa - Xã hội'),
('CD24',N'Nữ công gia chánh'),
('CD25',N'Nghệ thuật làm đẹp'),
('CD26',N'Du lịch'),
('CD27',N'Y Học dân tộc VN');

go
INSERT INTO [dbo].[User] (MaUser,HoTen, NgaySinh, GioiTinh, DienThoai, TaiKhoan, MatKhau, Email, DiaChi, PhanLoai)
VALUES
('admin01','','','','','admin','63e2a9478c782dee778b994942051fa0','','','admin'),
('user01',N'Nguyễn Văn A', '1990-01-01', N'Nam', '0912345678', 'nguyenvana@gmail.com', '25d55ad283aa400af464c76d713c07ad', 'nguyenvana@gmail.com', N'Hà Nội', N'user'),
('user02',N'Nguyễn Thị B', '1991-02-02', N'Nữ', '0923456789', 'nguyenthiba@gmail.com', '12345679', 'nguyenthiba@gmail.com', N'Hà Nội', N'user'),
('user03',N'Trần Văn C', '1992-03-03', N'Nam', '0934567890', 'tranvanca@gmail.com', '12345680', 'tranvanca@gmail.com', N'Đà Nẵng', N'user'),
('user04',N'Lê Thị D', '1993-04-04', N'Nữ', '0945678901', 'lethida@gmail.com', '12345681', 'lethida@gmail.com', N'Cần Thơ', N'user'),
('user05',N'Phan Văn E', '1994-05-05', N'Nam', '0956789012', 'phanvanea@gmail.com', '12345682', 'phanvanea@gmail.com', N'Nghệ An', N'user'),
('user06',N'Đỗ Thị F', '1995-06-06', N'Nữ', '0967890123', 'dothifa@gmail.com', '12345683', 'dothifa@gmail.com', N'Thanh Hóa', N'user'),
('user07',N'Hồ Văn G', '1996-07-07', N'Nam', '0978901234', 'hovangga@gmail.com', '12345684', 'hovangga@gmail.com', N'Quảng Bình', N'user'),
('user08',N'Nguyễn Thị H', '1997-08-08', N'Nữ', '0989012345', 'nguyenthikha@gmail.com', '12345685', 'nguyenthikha@gmail.com', N'Quảng Trị', N'user'),
('user09',N'Trần Văn I', '1998-09-09', N'Nam', '0990123456', 'tranvankia@gmail.com', '12345686', 'tranvankia@gmail.com', N'Thừa Thiên Huế', N'user'),
('user10',N'Lê Thị J', '1999-10-10', N'Nữ', '0911234567', 'lethijk@gmail.com', '12345687', 'lethijk@gmail.com', N'Đà Nẵng', N'user')

go
INSERT INTO dbo.Sach (MaSach,TenSach, GiaTien, MoTa, NgayXuatBan, SoLuongTon, MaChuDe, MaNXB)
VALUES
('Sach01',N'Điểm đến của gió', 100000, N'Cuốn tiểu thuyết kể về hành trình của một chàng trai trẻ đi tìm ý nghĩa của cuộc sống.', '2023-07-20', 10, 'CD1', 'NXB1'),
('Sach02',N'Cô gái trên chuyến tàu', 150000, N'Cuốn tiểu thuyết kể về câu chuyện tình yêu của một chàng trai và một cô gái gặp nhau trên chuyến tàu.', '2023-07-21', 20, 'CD2', 'NXB2'),
('Sach03',N'Mắt biếc', 200000, N'Cuốn tiểu thuyết kể về mối tình đầu trong sáng của một chàng trai và một cô gái ở miền Tây Nam Bộ.', '2023-07-22', 30, 'CD3', 'NXB3'),
('Sach04',N'Cuốn theo chiều gió', 250000, N'Cuốn tiểu thuyết kể về câu chuyện tình yêu của Scarlett OHara và Rhett Butler trong bối cảnh Nội chiến Hoa Kỳ.', '2023-07-23', 40, 'CD4', 'NXB4'),
('Sach05',N'Harry Potter và hòn đá phù thủy', 300000, N'Cuốn tiểu thuyết đầu tiên trong bộ truyện Harry Potter kể về câu chuyện của một cậu bé mồ côi được nhận vào học tại trường Phù thủy và Pháp sư Hogwarts.', '2023-07-24', 50, 'CD5', 'NXB5'),
('Sach06',N'Dế Mèn phiêu lưu kí', 350000, N'Cuốn truyện kể về cuộc phiêu lưu của Dế Mèn, một chú dế cường tráng và ham chơi.', '2023-07-25', 60, 'CD6', 'NXB6'),
('Sach07',N'Thánh Gióng', 400000, N'Câu chuyện dân gian kể về một cậu bé làng Gióng bỗng lớn nhanh như thổi và đánh đuổi giặc Ân xâm lược.', '2023-07-26', 70, 'CD7', 'NXB7'),
('Sach08',N'Truyện Kiều', 450000, N'Bộ truyện thơ lục bát nổi tiếng của đại thi hào Nguyễn Du kể về cuộc đời và số phận của Thúy Kiều.', '2023-07-27', 80, 'CD8', 'NXB8'),
('Sach09',N'Chinh phụ ngâm khúc', 500000, N'Bài thơ thất ngôn bát cú Đường luật kể về nỗi nhớ thương của người chinh phụ khi chồng đi lính.', '2023-07-28', 90, 'CD9', 'NXB9'),
('Sach10',N'Lão Hạc', 100000, N'Truyện ngắn kể về cuộc đời của một lão nông nghèo khổ phải bán con chó mà mình yêu quý.', '2023-07-29', 10, 'CD1', 'NXB1'),
('Sach11',N'Chuyện người con gái Nam Xương', 150000, N'Truyện kể về cuộc đời và cái chết oan khuất của Vũ Thị Thiết, một người phụ nữ đức hạnh nhưng bị chồng nghi ngờ và giết hại.', '2023-07-30', 20, 'CD2', 'NXB2'),
('Sach12',N'Bóng tối và ánh sáng', 200000, N'Cuốn tiểu thuyết kể về cuộc đấu tranh giữa thiện và ác trong thế giới con người.', '2023-08-01', 30, 'CD3', 'NXB3'),
('Sach13',N'Cuộc đời của Pi', 250000, N'Cuốn tiểu thuyết kể về cuộc hành trình đầy gian nan của một cậu bé Ấn Độ trên một chiếc thuyền cứu sinh cùng với một con hổ Bengal trong ba tháng.', '2023-08-02', 40, 'CD4', 'NXB4'),
('Sach14',N'Chúa tể những chiếc nhẫn', 300000, N'Cuốn tiểu thuyết giả tưởng kể về cuộc hành trình của một nhóm bạn để tiêu diệt Chúa tể Sauron, kẻ đang đe dọa thế giới.', '2023-08-03', 50, 'CD5', 'NXB5'),
('Sach15',N'Thiên đường và địa ngục', 350000, N'Cuốn tiểu thuyết kể về chuyến du hành của Dante Alighieri vào hai thế giới sau khi chết.', '2023-08-04', 60, 'CD6', 'NXB6'),
('Sach16',N'Hamlet', 400000, N'Tác phẩm kịch kể về câu chuyện của một hoàng tử Đan Mạch đang đấu tranh với sự nghi ngờ và trả thù.', '2023-08-05', 70, 'CD7', 'NXB7'),
('Sach17',N'Don Quixote', 450000, N'Tác phẩm tiểu thuyết kể về cuộc phiêu lưu của một hiệp sĩ già khúm núm và người hầu của anh ta.', '2023-08-06', 80, 'CD8', 'NXB8'),
('Sach18',N'Chiến tranh và hòa bình', 500000, N'Cuốn tiểu thuyết kể về cuộc đấu tranh giữa Napoleon và các thế lực châu Âu trong thế kỷ 19.', '2023-08-07', 90, 'CD9', 'NXB9'),
('Sach19',N'Tây du ký', 550000, N'Cuốn tiểu thuyết kể về cuộc hành trình của Đường Tăng sang Ấn Độ để lấy kinh Phật.', '2023-08-08', 100, 'CD10', 'NXB10')
go
INSERT INTO dbo.PhieuMuon (MaPhieuMuon, NgayMuon, PhiPhat, TinhTrang, MaUser)
VALUES
('PM015', '2023-07-21 08:00:00', 1,N'Đang Chờ', 'user04')
('PM014', '2023-07-21 08:00:00', 1,N'Đang Chờ', 'user03'),
('PM012', '2023-07-21 08:00:00', 1,N'Đang Chờ', 'user01'),
('PM013', '2023-07-21 08:00:00', 1,N'Đang Chờ', 'user01'),
('PM01', '2023-07-21 08:00:00', 1,N'Chấp nhận', 'user01'),
('PM02', '2023-07-22 08:00:00',1, N'Chấp nhận', 'user02'),
('PM03', '2023-07-23 08:00:00',1, N'Chấp nhận', 'user01'),
('PM04', '2023-07-24 08:00:00',1, N'Chấp nhận', 'user03'),
('PM05', '2023-07-25 08:00:00',1, N'Chấp nhận', 'user04'),
('PM06', '2023-07-26 08:00:00',1, N'Chấp nhận', 'user01'),
('PM07', '2023-07-27 08:00:00',1, N'Chấp nhận', 'user05'),
('PM08', '2023-07-28 08:00:00',1, N'Chấp nhận', 'user06'),
('PM09', '2023-07-29 08:00:00',1, N'Chấp nhận', 'user01'),
('PM10', '2023-07-30 08:00:00',1, N'Chấp nhận', 'user07')
go
-- Thêm dữ liệu vào bảng ChiTietPhieuMuon (Can them)
INSERT INTO [dbo].[ChiTietPhieuMuon] ([MaPhieuMuon], [MaSach], [SoLuong], [DonGia])
VALUES
	('PM015', 'Sach11', 2, 50000),
	('PM014', 'Sach11', 2, 50000),
	('PM012', 'Sach04', 2, 50000),
	('PM013', 'Sach05', 2, 50000),
    ('PM01', 'Sach01', 2, 50000),
    ('PM02', 'Sach15', 3, 4500),
    ('PM03', 'Sach03', 1, 6000),
    ('PM04', 'Sach08', 2, 5500),
	('PM05', 'Sach09', 2, 10000),
	('PM06', 'Sach07', 2, 6500),
    ('PM07', 'Sach04', 2, 8500),
    ('PM08', 'Sach06', 2, 9500),
    ('PM09', 'Sach14', 2, 5000),
	('PM10', 'Sach05', 2, 10000)
go
INSERT INTO [ThamGia] ([MaSach], [MaTacGia], [VaiTro], [ViTri])
VALUES
    ('Sach01', 'TG1', N'Tác giả', '1'),
    ('Sach01', 'TG2', N'Tác giả', '2'),
    ('Sach01', 'TG3', N'Tác giả', '1'),
    ('Sach01', 'TG14', N'Tác giả', '2'),
    ('Sach01', 'TG9', N'Tác giả', '1'),
    ('Sach02', 'TG23', N'Tác giả', '2'),
    ('Sach02', 'TG7', N'Tác giả', '1'),
    ('Sach03', 'TG19', N'Tác giả', '2'),
    ('Sach03', 'TG22', N'Tác giả', '1'),
    ('Sach04', 'TG21', N'Tác giả', '2')
go
--Lệnh gọi ra từ csdl:
CREATE PROCEDURE XoaSachVaLienQuan
    @MaSach nvarchar(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- Xóa dữ liệu từ bảng ThamGia
    DELETE FROM dbo.ThamGia WHERE MaSach = @MaSach;

    -- Xóa dữ liệu từ bảng ChiTietPhieuMuon
    DELETE FROM dbo.ChiTietPhieuMuon WHERE MaSach = @MaSach;

    -- Xóa dữ liệu từ bảng Sach
    DELETE FROM dbo.Sach WHERE MaSach = @MaSach;
END;
--Sửa thông tin sách:
CREATE PROCEDURE CapNhatThongTinSach
    @MaSach nvarchar(10),
    @TenSach nvarchar(100),
    @MaChuDe nvarchar(10),
    @MaNXB nvarchar(10),
    @NgayXuatBan datetime,
    @MaTacGia nvarchar(10),
    @SoLuong int,
    @GiaTien decimal(18, 0)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Sach
    SET
        TenSach = @TenSach,
        MaChuDe = @MaChuDe,
        MaNXB = @MaNXB,
        NgayXuatBan = @NgayXuatBan,
        SoLuongTon = @SoLuong,
        GiaTien = @GiaTien
    WHERE
        MaSach = @MaSach;

    -- Xóa và thêm lại thông tin tác giả để cập nhật nhanh chóng
    DELETE FROM dbo.ThamGia WHERE MaSach = @MaSach;
    INSERT INTO dbo.ThamGia (MaSach, MaTacGia) VALUES (@MaSach, @MaTacGia);
END;

SELECT a.MaSach, a.TenSach, b.TenChuDe, a.MaNXB, a.NgayXuatBan, c.MaTacGia,a.SoLuongTon, a.GiaTien FROM dbo.Sach a, dbo.ChuDe b, dbo.ThamGia c  
WHERE a.MaChuDe = b.MaChuDe and c.MaSach = a.MaSach
SELECT a.MaUser, a.HoTen, c.MaSach, b.NgayMuon, b.TinhTrang, b.PhiPhat FROM dbo.[User] a, dbo.ChiTietPhieuMuon c, dbo.PhieuMuon b
where a.MaUser = b.MaUser and b.MaPhieuMuon = c.MaPhieuMuon
select MaNXB from dbo.NhaXuatBan
select MaTacGia from dbo.TacGia

-- Du lieu tham gia
select * from dbo.PhieuMuon
--select * from dbo.TacGia
--select * from dbo.Sach
select * from dbo.ChiTietPhieuMuon
select * from dbo.NhaXuatBan
select * from dbo.[User]
select * from dbo.ThamGia
select * from dbo.ChuDe

select a.MaSach,a.TenSach,b.TenChuDe,a.MaNXB, a.NgayXuatBan, a.MaSach,a.SoLuongTon, a.GiaBan from dbo.Sach a, dbo.ChuDe b
where a.MaChuDe = b.MaChuDe
select * from dbo.Sach
select * from dbo.Chude
select * from dbo.ThamGia
select a.MaSach,a.TenSach,b.TenChuDe,a.MaNXB, a.NgayXuatBan, c.MaTacGia, a.SoLuongTon, a.GiaTien from dbo.Sach a, dbo.ChuDe b, dbo.ThamGia c
where a.MaChuDe = b.MaChuDe and a.MaSach = c.MaSach

select * from Sach
select MaTacGia from dbo.TacGia
select MaChuDe from dbo.ChuDe Where TenChuDe = N'Ngoại Ngữ'

SELECT a.MaSach, a.TenSach, b.TenChuDe, a.MaNXB, a.NgayXuatBan, a.SoLuongTon, a.GiaTien FROM dbo.Sach a INNER JOIN dbo.ChuDe b ON a.MaChuDe = b.MaChuDe  WHERE b.TenChuDe = N'Công nghệ thông tin'
SELECT a.MaUser, a.HoTen, d.TenSach, b.NgayMuon, b.TinhTrang, b.PhiPhat FROM dbo.[User] a, dbo.ChiTietPhieuMuon c, dbo.PhieuMuon b, dbo.Sach d
where a.MaUser = b.MaUser and b.MaPhieuMuon = c.MaPhieuMuon and c.MaSach = d.MaSach

SELECT a.MaUser, a.HoTen, d.TenSach, b.NgayMuon, b.TinhTrang, b.PhiPhat FROM dbo.[User] a 
                                   INNER JOIN dbo.PhieuMuon b ON a.MaUser = b.MaUser 
                                   INNER JOIN dbo.ChiTietPhieuMuon c ON b.MaPhieuMuon = c.MaPhieuMuon
                                   INNER JOIN dbo.Sach d ON c.MaSach = d.MaSach
SELECT a.MaUser, a.HoTen, d.TenSach, b.NgayMuon, b.TinhTrang, b.PhiPhat FROM dbo.[User] a, dbo.ChiTietPhieuMuon c, dbo.PhieuMuon b, dbo.Sach d
                    where a.MaUser = b.MaUser and b.MaPhieuMuon = c.MaPhieuMuon and c.MaSach = d.MaSach

SELECT b.MaPhieuMuon FROM dbo.PhieuMuon b
                INNER JOIN dbo.ChiTietPhieuMuon c ON b.MaPhieuMuon = c.MaPhieuMuon
                INNER JOIN dbo.Sach d ON c.MaSach = d.MaSach
				WHERE b.TinhTrang = N'Đang Chờ' and d.TenSach = N'Cuốn theo chiều gió 123' 
				and b.MaUser = 'user01'
go
SELECT b.MaPhieuMuon FROM dbo.PhieuMuon b 
                    INNER JOIN dbo.ChiTietPhieuMuon c ON b.MaPhieuMuon = c.MaPhieuMuon 
                    INNER JOIN dbo.Sach d ON c.MaSach = d.MaSach 
                    WHERE b.TinhTrang = N'Đang Chờ' AND d.TenSach = N'Cuốn theo chiều gió 123' AND b.MaUser = 'user01'
go
select * from NhaXuatBan
select * from TacGia
select * from ThamGia
select MatKhau from dbo.[User] where MaUser = 'user01'
select c.* from dbo.ChiTietPhieuMuon c, dbo.PhieuMuon b, dbo.[User] a 
where c.MaPhieuMuon = b.MaPhieuMuon and b.MaUser = a.MaUser and a.MaUser = 'User02'
select a.MaSach, a.MaTacGia, b.TenTacGia, a.ViTri, a.VaiTro from ThamGia a, TacGia b where a.MaTacGia = b.MaTacGia and a.MaTacGia = 'TG1'