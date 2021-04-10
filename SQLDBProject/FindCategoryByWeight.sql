CREATE FUNCTION [dbo].[FindCategoryByWeight]
(
	@weight decimal(9,2)
)
RETURNS int
begin
	declare @result int=-1
	select @result=max(Id) from Categories where MinWeight<=@weight
	return @result
end
