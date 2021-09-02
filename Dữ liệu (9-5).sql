﻿create table KHU(
	IdKhu nvarchar(50) not null,
	TenKhu nvarchar(50) not null

	constraint PK_Khu primary key (IdKhu) 
)
create table PHONGHOC(
	IdPhong nvarchar(50) not null,
	IdKhu nvarchar(50) not null,
	STTPH int not null,
	X int not null,
	Y int not null,
	Z int not null

	constraint PK_PhongHoc primary key (IdPhong,STTPH)
	constraint FK_PhongHoc foreign key (IdKhu) references KHU (IdKhu)
)

create table CAUTHANG(
	IdCauThang nvarchar(50) not null,
	IdKhu nvarchar(50) not null,
	STTCT int not null,
	X int not null,
	Y int not null,
	Z int not null

	constraint PK_CauThang primary key (IdCauThang,STTCT)
	constraint FK_CauThang foreign key (IdKhu) references KHU (IdKhu)
)

create table ADDNODE(
	IdAddNode nvarchar(50) not null,
	IdKhu nvarchar(50) not null,
	X int not null,
	Y int not null,

	constraint PK_AddNode primary key (IdAddNode),
	constraint FK_AddNode foreign key (IdKhu) references KHU (IdKhu)
)

insert into KHU
values ('1', 'EC') ,
('2', 'EM'),
('3', 'CM'),
('4', 'CC'),
('5', 'BT'),
('6', 'F'),
('7', 'BS'),
('8', 'AddNode');

insert into PHONGHOC
values('EM101', '2' ,2, 1698, 88, 0),
('EM102', '2' , 3, 1720, 88, 0),
('EM103', '2' , 4, 1846, 88, 0),
('EM104', '2' , 5, 1885, 88, 0),
('EM201', '2' , 8, 1698, 88, 10),
('EM202', '2' , 9, 1720, 88, 10),
('EM203', '2' , 10, 1846, 88, 10),
('EM204', '2' , 11, 1885, 88, 10),

('EC110B', '1' , 1, 1903, 294, 0),
('EC110A', '1' , 2, 1903, 322, 0),
('EC_Phong May Tinh Chuyen Nganh', '1' , 4, 1903, 425, 0),
('EC112', '1' , 5, 1903, 460, 0),
('EC205', '1' , 6, 1903, 294, 10),
('EC206', '1' , 7, 1903, 322, 10),
('EC207', '1' , 9, 1903, 425, 10),
('EC208', '1' , 10, 1903, 460, 10),
('EC301B', '1' , 11, 1903, 294, 20),
('EC301A', '1' , 12, 1903, 322, 20),
('EC302', '1' , 14, 1903, 425, 20),
('EC303', '1' , 15, 1903, 460, 20),
('EC401', '1' , 16, 1903, 294, 30),
('EC402', '1' , 17, 1903, 322, 30),
('EC403', '1' , 19, 1903, 425, 30),
('EC404', '1' , 20, 1903, 460, 30),

('CM128', '3' , 1, 1761, 568, 0),
('CM_Van phong chat luong cao tieng Nhat', '3'  ,2 , 1761, 584, 0),
('CM127', '3' , 4, 1761, 619, 0),
('CM126', '3' , 5, 1761, 631, 0),
('CM125', '3' , 6, 1761, 642, 0),
('CM231', '3' , 7, 1761, 568, 10),
('CM230', '3' , 8, 1761, 584, 10),
('CM229', '3' , 10, 1761, 619, 10),
('CM228', '3' , 11, 1761, 631, 10),

('CC101', '4' , 1, 1696, 711, 0),
('CC102', '4' , 2, 1696, 740, 0),
('CC103', '4' , 3, 1696, 766, 0),
('CC_Thi nghiem he thong nhung', '4' , 4, 1696, 793, 0),
('CC105', '4' , 5, 1696, 817, 0),
('CC106', '4' , 6, 1696, 842, 0),
('CC107', '4' , 7, 1696, 867, 0),

('BT101', '5', 2, 1303, 1000, 0),
('BT102', '5', 3, 1335, 1000, 0),
('BT103', '5', 4, 1369, 1000, 0),
('BT104', '5', 6, 1414, 1000, 0),
('BT105', '5', 7, 1450, 1000, 0),
('BT106', '5', 8, 1486, 1000, 0),
('BT201', '5', 11, 1303, 1000, 10),
('BT202', '5', 12, 1335, 1000, 10),
('BT203', '5', 13, 1369, 1000, 10),
('BT204', '5', 15, 1414, 1000, 10),
('BT205', '5', 16, 1450, 1000, 10),
('BT206', '5', 17, 1486, 1000, 10),
('BT301', '5', 20, 1303, 1000, 20),
('BT302A', '5', 21, 1335, 1000, 20),
('BT302B', '5', 22, 1369, 1000, 20),
('BT303', '5', 24, 1414, 1000, 20),
('BT304', '5', 25, 1450, 1000, 20),
('BT305', '5', 26, 1486, 1000, 20),

('BS107', '7', 3, 1458, 953, 0),
('BS108', '7', 2, 1398, 953, 0),
('BS109', '7', 1, 1337, 953, 0),
('BS207', '7', 6, 1458, 953, 10),
('BS208', '7', 5, 1398, 953, 10),
('BS209', '7', 4, 1337, 953, 10),

('F101', '6', 2 , 1207, 921, 0),
('F102', '6', 3 , 1207, 888, 0),
('F103', '6', 4 , 1207, 850, 0),
('F104', '6', 6 , 1207, 821, 0),
('F105', '6', 7 , 1207, 795, 0),
('F106', '6', 8 , 1207, 765, 0),
('F107', '6', 9 , 1207, 740, 0),
('F108', '6', 11 , 1250, 707, 0),
('F109', '6', 12 , 1250, 668, 0),
('F110', '6', 13 , 1250, 629, 0),

('F201', '6', 15 , 1207, 921, 10),
('F202', '6', 16 , 1207, 888, 10),
('F203', '6', 17 , 1207, 850, 10),
('F204', '6', 19 , 1207, 821, 10),
('F205', '6', 20 , 1207, 795, 10),
('F206', '6', 21 , 1207, 765, 10),
('F207', '6', 22 , 1207, 740, 10),
('F208', '6', 24 , 1250, 707, 10),
('F209', '6', 25 , 1250, 668, 10),
('F210', '6', 26 , 1250, 629, 10),

('F301', '6', 28 , 1207, 921, 20),
('F302', '6', 29 , 1207, 888, 20),
('F303', '6', 30 , 1207, 850, 20),
('F304', '6', 32 , 1207, 821, 20),
('F305', '6', 33 , 1207, 795, 20),
('F306', '6', 34 , 1207, 765, 20),
('F307', '6', 35 , 1207, 740, 20),
('F308', '6', 37 , 1250, 707, 20),
('F309', '6', 38 , 1250, 668, 20),
('F310', '6', 39 , 1250, 629, 20),

('F401', '6', 41 , 1207, 921, 30),
('F402', '6', 42 , 1207, 888, 30),
('F403', '6', 43 , 1207, 850, 30),
('F404', '6', 45 , 1207, 821, 30),
('F405', '6', 46 , 1207, 795, 30),
('F406', '6', 47 , 1207, 765, 30),
('F407', '6', 48 , 1207, 740, 30),
('F408', '6', 50 , 1250, 707, 30),
('F409', '6', 51 , 1250, 668, 30),
('F410', '6', 52 , 1250, 629, 30);

insert into CAUTHANG
values 
('EM_CT11', '2', 1, 1684 , 88, 0),
('EM_CT21', '2', 6, 1885 , 90, 0),
('EM_CT12', '2', 7, 1684 , 88, 10),
('EM_CT22', '2', 12, 1885 , 90, 10),

('EC_CT11', '1', 3, 1903 , 387, 0),
('EC_CT12', '1', 8, 1918 , 387, 10),
('EC_CT13', '1', 13, 1918 , 387, 20),
('EC_CT14', '1', 18, 1918 , 387, 30),

('CM_CT11', '3', 3, 1761, 604, 0),
('CM_CT12', '3', 9, 1761, 604, 10),

('BT_CT11', '5', 1, 1276, 1000, 0),
('BT_CT21', '5', 5, 1395, 1000, 0),
('BT_CT31', '5', 9, 1518, 1000, 0),
('BT_CT12', '5', 10, 1276, 1000, 10),
('BT_CT22', '5', 14, 1395, 1000, 10),
('BT_CT32', '5', 18, 1518, 1000, 10),
('BT_CT13', '5', 19, 1276, 1000, 20),
('BT_CT23', '5', 23, 1395, 1000, 20),
('BT_CT33', '5', 27, 1518, 1000, 20),

('F_CT11', '6', 1, 1207, 960, 0),
('F_CT21', '6', 5, 1207, 855, 0),
('F_CT31', '6', 10, 1207, 707, 0),
('F_CT12', '6', 14, 1207, 960, 10),
('F_CT22', '6', 18, 1207, 855, 10),
('F_CT32', '6', 23, 1207, 707, 10),
('F_CT13', '6', 27, 1207, 960, 20),
('F_CT23', '6', 31, 1207, 855, 20),
('F_CT33', '6', 36, 1207, 707, 20),
('F_CT14', '6', 40, 1207, 960, 30),
('F_CT24', '6', 44, 1207, 855, 30),
('F_CT34', '6', 49, 1207, 707, 30);

insert into ADDNODE
values ('DP_EM0', '2', 1664, 94),	-- Điểm dp5 cũ
('DP_CM0', '3', 1667, 557),	-- Điểm dp1 cũ
('DP_CC0', '4', 1664, 969),
('DP1','8',1272,569),
('DP2','8',1284,784),
('DP_HT','8',1304,853)
