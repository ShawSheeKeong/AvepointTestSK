CREATE TABLE T_Cafe_Employee (
    id INT IDENTITY(1,1) PRIMARY KEY,
    cafe_id UNIQUEIDENTIFIER NOT NULL,
    employee_id NVARCHAR(10) NOT NULL,
    start_date DATE NOT NULL DEFAULT GETDATE(),
    end_date DATE NULL,
    is_deleted BIT NOT NULL,
    created_time DATETIME DEFAULT GETDATE(),
    created_by NVARCHAR(200) NOT NULL,
    last_updated_time DATETIME DEFAULT GETDATE(),
    last_updated_by NVARCHAR(200) NOT NULL
    FOREIGN KEY (cafe_id) REFERENCES T_Cafe(id) ON DELETE CASCADE,
    FOREIGN KEY (employee_id) REFERENCES T_Employee(id) ON DELETE CASCADE,
    UNIQUE (cafe_id, employee_id)
);

ALTER TABLE T_Cafe_Employee ADD CONSTRAINT DF_T_Cafe_Employee_IsDeleted DEFAULT 0 FOR is_deleted;


INSERT INTO T_Cafe_Employee (cafe_id, employee_id, start_date, is_deleted, created_by, last_updated_by)
VALUES
('01af61f5-2aad-459a-9fcd-5e90d8a15907', 'UIA000001', '2023-01-01', 0, 'admin', 'admin'),
('01af61f5-2aad-459a-9fcd-5e90d8a15907', 'UIA000023', '2023-02-15', 0, 'admin', 'admin');