CREATE FUNCTION [dbo].[FindMaxWeight]
(
	@minWeight decimal(9,2)
)
RETURNS decimal(9,2)
begin
	declare @result decimal(9,2)
	select @result=min(MinWeight) from Categories where MinWeight>@minWeight
	if @result is null set @result=-1
	return @result
end