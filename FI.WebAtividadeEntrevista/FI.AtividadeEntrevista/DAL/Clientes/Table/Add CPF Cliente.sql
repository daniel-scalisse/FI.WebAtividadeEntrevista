﻿USE [C:\USERS\DANIEL\DOWNLOADS\FI.WEBATIVIDADEENTREVISTA\FI.WEBATIVIDADEENTREVISTA\APP_DATA\BANCODEDADOS.MDF]

--SELECT DB_NAME()

ALTER TABLE dbo.CLIENTES
    ADD CPF CHAR(11) NOT NULL
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_CLIENTES_CPF ON dbo.CLIENTES (CPF ASC)
GO