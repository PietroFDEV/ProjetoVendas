USE VendasDB;

-- Cliente Table
CREATE TABLE Cliente (
    idCliente INT PRIMARY KEY IDENTITY,
    nmCliente NVARCHAR(100) NOT NULL,
    Cidade NVARCHAR(100) NOT NULL
);

-- Produto Table
CREATE TABLE Produto (
    idProduto INT PRIMARY KEY IDENTITY,
    dscProduto NVARCHAR(200) NOT NULL,
    vlrUnitario FLOAT NOT NULL
);

-- Venda Table
CREATE TABLE Venda (
    idVenda INT PRIMARY KEY IDENTITY,
    idCliente INT NOT NULL,
    idProduto INT NOT NULL,
    qtdVenda INT NOT NULL,
    vlrUnitarioVenda FLOAT NOT NULL,
    dthVenda DATETIME NOT NULL,
    vlrTotalVenda FLOAT NOT NULL,
    CONSTRAINT FK_Venda_Cliente FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente),
    CONSTRAINT FK_Venda_Produto FOREIGN KEY (idProduto) REFERENCES Produto(idProduto)
);