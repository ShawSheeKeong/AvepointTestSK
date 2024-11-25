CREATE TABLE T_Cafe (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    name NVARCHAR(200) NOT NULL,
    description NVARCHAR(256),
    logo NVARCHAR(MAX),
    location NVARCHAR(50) NOT NULL,
    is_deleted BIT NOT NULL,
    created_time DATETIME DEFAULT GETDATE(),
    created_by NVARCHAR(200) NOT NULL,
    last_updated_time DATETIME DEFAULT GETDATE(),
    last_updated_by NVARCHAR(200) NOT NULL
);

ALTER TABLE T_Cafe ADD CONSTRAINT DF_T_Cafe_IsDeleted DEFAULT 0 FOR is_deleted;

INSERT INTO T_Cafe (id, name, description, logo, location, is_deleted, created_by, last_updated_by)
VALUES 
('01af61f5-2aad-459a-9fcd-5e90d8a15907','Cafe Mocha', 'A cozy coffee shop.', 'https://www.google.com/url?sa=i&url=https%3A%2F%2Fcommons.wikimedia.org%2Fwiki%2FFile%3AMocha_logo.svg&psig=AOvVaw3TcYEYdjz6lk51troZvcX0&ust=1732627939484000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCNDcqajM94kDFQAAAAAdAAAAABAE', 
'Downtown', 0, 'Admin', 'Admin');