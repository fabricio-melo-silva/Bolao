CREATE FUNCTION [dbo].[FN_CRIPTOGRAFAR_SENHA] (@senha NVARCHAR(50))
	RETURNS NVARCHAR(1000)
AS
BEGIN
	DECLARE
		@key NVARCHAR(256),
		@senhaCriptografada NVARCHAR(1000)

	SET @senhaCriptografada = UPPER(master.dbo.fn_varbintohexstr(HASHBYTES('SHA1', @senha)))
	SET @senhaCriptografada = RIGHT(@senhaCriptografada, LEN(@senhaCriptografada) - 2)

	RETURN @senhaCriptografada
END
