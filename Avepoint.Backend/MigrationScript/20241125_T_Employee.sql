CREATE TABLE T_Employee (
    id NVARCHAR(10) PRIMARY KEY,
    name NVARCHAR(200) NOT NULL,
    email_address NVARCHAR(100) UNIQUE NOT NULL,
    phone_number NVARCHAR(15) UNIQUE NOT NULL,
    gender NVARCHAR(10) CHECK (gender IN ('Male', 'Female')) NOT NULL,
    is_deleted BIT NOT NULL,
    created_time DATETIME DEFAULT GETDATE(),
    created_by NVARCHAR(200) NOT NULL,
    last_updated_time DATETIME DEFAULT GETDATE(),
    last_updated_by NVARCHAR(200) NOT NULL
);

ALTER TABLE T_Employee ADD CONSTRAINT DF_T_Employee_IsDeleted DEFAULT 0 FOR is_deleted;

INSERT INTO T_Employee (id, name, email_address, phone_number, gender, is_deleted, created_by, last_updated_by)
VALUES 
('UIA000001', 'John Doe', 'john.doe@test.com', '91234567', 'Male', 0, 'admin', 'admin'),
('UIA000023', 'Jane Smith', 'jane.smith@test.com', '92345678', 'Female', 0, 'admin', 'admin');
