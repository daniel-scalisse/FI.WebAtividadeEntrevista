﻿USE [C:\USERS\DANIEL\DOWNLOADS\FI.WEBATIVIDADEENTREVISTA\FI.WEBATIVIDADEENTREVISTA\APP_DATA\BANCODEDADOS.MDF]
GO

CREATE OR ALTER PROC dbo.FI_SP_DelCliente
	--@qtBenef BIGINT OUTPUT,
	@ID BIGINT
AS
	--SET NOCOUNT ON

	BEGIN
		--SELECT @qtBenef=COUNT(*) FROM dbo.BENEFICIARIOS WHERE IDCLIENTE=@ID
		--IF @qtBenef=0
			DELETE dbo.CLIENTES WHERE ID = @ID
	END