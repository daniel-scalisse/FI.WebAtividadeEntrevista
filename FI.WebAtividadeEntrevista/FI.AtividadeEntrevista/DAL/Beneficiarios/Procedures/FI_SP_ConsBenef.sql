﻿USE [C:\USERS\DANIEL\DOWNLOADS\FI.WEBATIVIDADEENTREVISTA\FI.WEBATIVIDADEENTREVISTA\APP_DATA\BANCODEDADOS.MDF]
GO

CREATE OR ALTER PROC dbo.FI_SP_ConsBenef
	@ID BIGINT
AS
BEGIN
	SELECT ID, CPF, NOME, IDCLIENTE FROM dbo.BENEFICIARIOS WITH(NOLOCK) WHERE ID = @ID
END