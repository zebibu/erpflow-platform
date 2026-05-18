IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE CompanyName = 'Brescia Industrial Parts SRL')
	INSERT INTO Suppliers (CompanyName, VatNumber, Email, Phone, City, Country)
	VALUES ('Brescia Industrial Parts SRL', 'IT12345678901', 'info@bresciaparts.it', '+39 030 111222', 'Brescia', 'Italy');

IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE CompanyName = 'Milano Componenti SPA')
	INSERT INTO Suppliers (CompanyName, VatNumber, Email, Phone, City, Country)
	VALUES ('Milano Componenti SPA', 'IT98765432109', 'sales@milanocomponenti.it', '+39 02 333444', 'Milano', 'Italy');

IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE CompanyName = 'Torino Automazione SNC')
	INSERT INTO Suppliers (CompanyName, VatNumber, Email, Phone, City, Country)
	VALUES ('Torino Automazione SNC', 'IT24681357902', 'commerciale@torinoauto.it', '+39 011 555010', 'Torino', 'Italy');

IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE CompanyName = 'Veneto Ricambi SRL')
	INSERT INTO Suppliers (CompanyName, VatNumber, Email, Phone, City, Country)
	VALUES ('Veneto Ricambi SRL', 'IT10293847566', 'support@venetoricambi.it', '+39 041 880990', 'Venezia', 'Italy');

IF NOT EXISTS (SELECT 1 FROM Customers WHERE CompanyName = 'Alfa Manufacturing SRL')
	INSERT INTO Customers (CompanyName, VatNumber, FiscalCode, Email, Phone, City, Province, Country)
	VALUES ('Alfa Manufacturing SRL', 'IT11122233344', 'ALFAMFG80A01B157X', 'orders@alfamfg.it', '+39 030 555666', 'Brescia', 'BS', 'Italy');

IF NOT EXISTS (SELECT 1 FROM Customers WHERE CompanyName = 'Nord Logistic SPA')
	INSERT INTO Customers (CompanyName, VatNumber, FiscalCode, Email, Phone, City, Province, Country)
	VALUES ('Nord Logistic SPA', 'IT55566677788', 'NRDLGS80A01F205X', 'admin@nordlogistic.it', '+39 02 777888', 'Milano', 'MI', 'Italy');

IF NOT EXISTS (SELECT 1 FROM Customers WHERE CompanyName = 'Officine Lombarde SRL')
	INSERT INTO Customers (CompanyName, VatNumber, FiscalCode, Email, Phone, City, Province, Country)
	VALUES ('Officine Lombarde SRL', 'IT33344455566', 'OFFLMB80A01F704Y', 'procurement@officinelombarde.it', '+39 035 444111', 'Bergamo', 'BG', 'Italy');

IF NOT EXISTS (SELECT 1 FROM Customers WHERE CompanyName = 'Trasporti Emilia SPA')
	INSERT INTO Customers (CompanyName, VatNumber, FiscalCode, Email, Phone, City, Province, Country)
	VALUES ('Trasporti Emilia SPA', 'IT77788899900', 'TRSEML80A01A944V', 'fleet@trasportiemilia.it', '+39 0522 900123', 'Reggio Emilia', 'RE', 'Italy');

IF NOT EXISTS (SELECT 1 FROM Warehouses WHERE Name = 'Magazzino Centrale')
	INSERT INTO Warehouses (Name, Location, Address, City, Province, PostalCode)
	VALUES ('Magazzino Centrale', 'Headquarters', 'Via Industriale 12', 'Brescia', 'BS', '25100');

IF NOT EXISTS (SELECT 1 FROM Warehouses WHERE Name = 'Magazzino Nord')
	INSERT INTO Warehouses (Name, Location, Address, City, Province, PostalCode)
	VALUES ('Magazzino Nord', 'Secondary Warehouse', 'Via Milano 33', 'Bergamo', 'BG', '24100');

IF NOT EXISTS (SELECT 1 FROM Warehouses WHERE Name = 'Magazzino Sud')
	INSERT INTO Warehouses (Name, Location, Address, City, Province, PostalCode)
	VALUES ('Magazzino Sud', 'Southern Hub', 'Via Adriatica 81', 'Bologna', 'BO', '40100');

IF NOT EXISTS (SELECT 1 FROM Products WHERE SKU = 'ERP-AX-100')
	INSERT INTO Products (SKU, Name, Category, Description, UnitPrice, VatRate, MinimumStockLevel, SupplierId)
	VALUES ('ERP-AX-100', 'Pompa Idraulica AX100', 'Hydraulics', 'Industrial hydraulic pump', 185.00, 22.00, 10, (SELECT TOP 1 Id FROM Suppliers WHERE CompanyName = 'Brescia Industrial Parts SRL'));

IF NOT EXISTS (SELECT 1 FROM Products WHERE SKU = 'ERP-VL-220')
	INSERT INTO Products (SKU, Name, Category, Description, UnitPrice, VatRate, MinimumStockLevel, SupplierId)
	VALUES ('ERP-VL-220', 'Valvola Pressione VL220', 'Valves', 'Pressure control valve', 74.50, 22.00, 20, (SELECT TOP 1 Id FROM Suppliers WHERE CompanyName = 'Milano Componenti SPA'));

IF NOT EXISTS (SELECT 1 FROM Products WHERE SKU = 'ERP-SN-310')
	INSERT INTO Products (SKU, Name, Category, Description, UnitPrice, VatRate, MinimumStockLevel, SupplierId)
	VALUES ('ERP-SN-310', 'Sensore Temperatura SN310', 'Sensors', 'Thermal monitoring sensor', 129.90, 22.00, 15, (SELECT TOP 1 Id FROM Suppliers WHERE CompanyName = 'Torino Automazione SNC'));

IF NOT EXISTS (SELECT 1 FROM Products WHERE SKU = 'ERP-MT-500')
	INSERT INTO Products (SKU, Name, Category, Description, UnitPrice, VatRate, MinimumStockLevel, SupplierId)
	VALUES ('ERP-MT-500', 'Motore Lineare MT500', 'Motors', 'Linear movement motor', 540.00, 22.00, 6, (SELECT TOP 1 Id FROM Suppliers WHERE CompanyName = 'Veneto Ricambi SRL'));

IF NOT EXISTS (SELECT 1 FROM Products WHERE SKU = 'ERP-CN-120')
	INSERT INTO Products (SKU, Name, Category, Description, UnitPrice, VatRate, MinimumStockLevel, SupplierId)
	VALUES ('ERP-CN-120', 'Cuscinetto CN120', 'Bearings', 'Heavy duty bearing', 18.90, 22.00, 40, (SELECT TOP 1 Id FROM Suppliers WHERE CompanyName = 'Brescia Industrial Parts SRL'));

IF NOT EXISTS (SELECT 1 FROM Products WHERE SKU = 'ERP-EL-900')
	INSERT INTO Products (SKU, Name, Category, Description, UnitPrice, VatRate, MinimumStockLevel, SupplierId)
	VALUES ('ERP-EL-900', 'Modulo Controllo EL900', 'Electronics', 'Control module for automation lines', 310.00, 22.00, 8, (SELECT TOP 1 Id FROM Suppliers WHERE CompanyName = 'Torino Automazione SNC'));

IF NOT EXISTS (
	SELECT 1 FROM ProductStocks ps
	JOIN Products p ON p.Id = ps.ProductId
	JOIN Warehouses w ON w.Id = ps.WarehouseId
	WHERE p.SKU = 'ERP-AX-100' AND w.Name = 'Magazzino Centrale')
	INSERT INTO ProductStocks (ProductId, WarehouseId, Quantity)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-AX-100'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Centrale'), 42);

IF NOT EXISTS (
	SELECT 1 FROM ProductStocks ps
	JOIN Products p ON p.Id = ps.ProductId
	JOIN Warehouses w ON w.Id = ps.WarehouseId
	WHERE p.SKU = 'ERP-VL-220' AND w.Name = 'Magazzino Nord')
	INSERT INTO ProductStocks (ProductId, WarehouseId, Quantity)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-VL-220'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Nord'), 18);

IF NOT EXISTS (
	SELECT 1 FROM ProductStocks ps
	JOIN Products p ON p.Id = ps.ProductId
	JOIN Warehouses w ON w.Id = ps.WarehouseId
	WHERE p.SKU = 'ERP-SN-310' AND w.Name = 'Magazzino Centrale')
	INSERT INTO ProductStocks (ProductId, WarehouseId, Quantity)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-SN-310'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Centrale'), 27);

IF NOT EXISTS (
	SELECT 1 FROM ProductStocks ps
	JOIN Products p ON p.Id = ps.ProductId
	JOIN Warehouses w ON w.Id = ps.WarehouseId
	WHERE p.SKU = 'ERP-MT-500' AND w.Name = 'Magazzino Nord')
	INSERT INTO ProductStocks (ProductId, WarehouseId, Quantity)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-MT-500'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Nord'), 8);

IF NOT EXISTS (
	SELECT 1 FROM ProductStocks ps
	JOIN Products p ON p.Id = ps.ProductId
	JOIN Warehouses w ON w.Id = ps.WarehouseId
	WHERE p.SKU = 'ERP-CN-120' AND w.Name = 'Magazzino Sud')
	INSERT INTO ProductStocks (ProductId, WarehouseId, Quantity)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-CN-120'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Sud'), 120);

IF NOT EXISTS (
	SELECT 1 FROM ProductStocks ps
	JOIN Products p ON p.Id = ps.ProductId
	JOIN Warehouses w ON w.Id = ps.WarehouseId
	WHERE p.SKU = 'ERP-EL-900' AND w.Name = 'Magazzino Centrale')
	INSERT INTO ProductStocks (ProductId, WarehouseId, Quantity)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-EL-900'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Centrale'), 14);

IF NOT EXISTS (SELECT 1 FROM StockMovements WHERE ReferenceNumber = 'RCV-DEMO-0001')
	INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, ReferenceNumber, Reason, CreatedAt)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-AX-100'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Centrale'), 0, 50, 'RCV-DEMO-0001', 'Initial supplier receipt', DATEADD(DAY, -10, GETDATE()));

IF NOT EXISTS (SELECT 1 FROM StockMovements WHERE ReferenceNumber = 'RCV-DEMO-0002')
	INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, ReferenceNumber, Reason, CreatedAt)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-VL-220'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Nord'), 0, 25, 'RCV-DEMO-0002', 'Initial supplier receipt', DATEADD(DAY, -9, GETDATE()));

IF NOT EXISTS (SELECT 1 FROM StockMovements WHERE ReferenceNumber = 'RCV-DEMO-0003')
	INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, ReferenceNumber, Reason, CreatedAt)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-SN-310'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Centrale'), 0, 30, 'RCV-DEMO-0003', 'Initial supplier receipt', DATEADD(DAY, -8, GETDATE()));

IF NOT EXISTS (SELECT 1 FROM StockMovements WHERE ReferenceNumber = 'RCV-DEMO-0004')
	INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, ReferenceNumber, Reason, CreatedAt)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-MT-500'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Nord'), 0, 10, 'RCV-DEMO-0004', 'Initial supplier receipt', DATEADD(DAY, -7, GETDATE()));

IF NOT EXISTS (SELECT 1 FROM StockMovements WHERE ReferenceNumber = 'RCV-DEMO-0005')
	INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, ReferenceNumber, Reason, CreatedAt)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-CN-120'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Sud'), 0, 120, 'RCV-DEMO-0005', 'Initial supplier receipt', DATEADD(DAY, -6, GETDATE()));

IF NOT EXISTS (SELECT 1 FROM StockMovements WHERE ReferenceNumber = 'RCV-DEMO-0006')
	INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, ReferenceNumber, Reason, CreatedAt)
	VALUES ((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-EL-900'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Centrale'), 0, 15, 'RCV-DEMO-0006', 'Initial supplier receipt', DATEADD(DAY, -5, GETDATE()));

IF NOT EXISTS (SELECT 1 FROM Orders WHERE OrderNumber = 'ORD-DEMO-0001')
BEGIN
	INSERT INTO Orders (OrderNumber, CustomerId, OrderDate, Status, NetAmount, VatAmount, TotalAmount)
	VALUES ('ORD-DEMO-0001', (SELECT TOP 1 Id FROM Customers WHERE CompanyName = 'Alfa Manufacturing SRL'), DATEADD(DAY, -7, GETDATE()), 1, 370.00, 81.40, 451.40);

	INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice, VatRate, LineTotal)
	VALUES
	((SELECT TOP 1 Id FROM Orders WHERE OrderNumber = 'ORD-DEMO-0001'), (SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-AX-100'), 2, 185.00, 22.00, 451.40);
END;

IF NOT EXISTS (SELECT 1 FROM Orders WHERE OrderNumber = 'ORD-DEMO-0002')
BEGIN
	INSERT INTO Orders (OrderNumber, CustomerId, OrderDate, Status, NetAmount, VatAmount, TotalAmount)
	VALUES ('ORD-DEMO-0002', (SELECT TOP 1 Id FROM Customers WHERE CompanyName = 'Nord Logistic SPA'), DATEADD(DAY, -3, GETDATE()), 1, 372.50, 81.95, 454.45);

	INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice, VatRate, LineTotal)
	VALUES
	((SELECT TOP 1 Id FROM Orders WHERE OrderNumber = 'ORD-DEMO-0002'), (SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-VL-220'), 5, 74.50, 22.00, 454.45);
END;

IF NOT EXISTS (SELECT 1 FROM Orders WHERE OrderNumber = 'ORD-DEMO-0003')
BEGIN
	INSERT INTO Orders (OrderNumber, CustomerId, OrderDate, Status, NetAmount, VatAmount, TotalAmount)
	VALUES ('ORD-DEMO-0003', (SELECT TOP 1 Id FROM Customers WHERE CompanyName = 'Officine Lombarde SRL'), DATEADD(DAY, -1, GETDATE()), 0, 389.70, 85.73, 475.43);

	INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice, VatRate, LineTotal)
	VALUES
	((SELECT TOP 1 Id FROM Orders WHERE OrderNumber = 'ORD-DEMO-0003'), (SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-SN-310'), 3, 129.90, 22.00, 475.43);
END;

IF NOT EXISTS (SELECT 1 FROM Invoices WHERE InvoiceNumber = 'FT-DEMO-0001')
BEGIN
	INSERT INTO Invoices (InvoiceNumber, OrderId, InvoiceDate, NetAmount, VatAmount, TotalAmount, PaymentStatus, PdfUrl)
	VALUES ('FT-DEMO-0001', (SELECT TOP 1 Id FROM Orders WHERE OrderNumber = 'ORD-DEMO-0001'), DATEADD(DAY, -6, GETDATE()), 370.00, 81.40, 451.40, 0, '');
END;

IF NOT EXISTS (SELECT 1 FROM Invoices WHERE InvoiceNumber = 'FT-DEMO-0002')
BEGIN
	INSERT INTO Invoices (InvoiceNumber, OrderId, InvoiceDate, NetAmount, VatAmount, TotalAmount, PaymentStatus, PdfUrl)
	VALUES ('FT-DEMO-0002', (SELECT TOP 1 Id FROM Orders WHERE OrderNumber = 'ORD-DEMO-0002'), DATEADD(DAY, -2, GETDATE()), 372.50, 81.95, 454.45, 1, '');
END;

IF NOT EXISTS (SELECT 1 FROM StockMovements WHERE ReferenceNumber = 'ORD-DEMO-0001')
BEGIN
	INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, ReferenceNumber, Reason, CreatedAt)
	VALUES
	((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-AX-100'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Centrale'), 1, 2, 'ORD-DEMO-0001', 'Order confirmed', DATEADD(DAY, -7, GETDATE()));
END;

IF NOT EXISTS (SELECT 1 FROM StockMovements WHERE ReferenceNumber = 'ORD-DEMO-0002')
BEGIN
	INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, ReferenceNumber, Reason, CreatedAt)
	VALUES
	((SELECT TOP 1 Id FROM Products WHERE SKU = 'ERP-VL-220'), (SELECT TOP 1 Id FROM Warehouses WHERE Name = 'Magazzino Nord'), 1, 5, 'ORD-DEMO-0002', 'Order confirmed', DATEADD(DAY, -3, GETDATE()));
END;
