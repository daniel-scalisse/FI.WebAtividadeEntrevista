﻿USE [C:\USERS\DANIEL\DOWNLOADS\FI.WEBATIVIDADEENTREVISTA\FI.WEBATIVIDADEENTREVISTA\APP_DATA\BANCODEDADOS.MDF]
GO

CREATE OR ALTER PROC dbo.FI_SP_ConsBenefPorCliente
	@IDCLIENTE BIGINT
AS
BEGIN
	SELECT ID, CPF, NOME FROM dbo.BENEFICIARIOS WITH(NOLOCK) WHERE IDCLIENTE = @IDCLIENTE
END