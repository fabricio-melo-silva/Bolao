function NumericFormat(field, decimalPlaces, decimalSeparator, triggeredEvent) 
{
	var key = triggeredEvent.keyCode;
	
	if (key == 37 || key == 39) return;
	
	var regex = new RegExp("[^0-9]", "gi");
	var vr = field.value.replace(regex, ""); 

	if (vr.length <= decimalPlaces)
	{
	 	field.value = vr; 
	}
	else
	{
	 	var integerPart = vr;
	 	var decimalPart = "";
	 	var groupSize = 3;
	 	var groupCounter = 0;
	 	
	 	if (decimalPlaces > 0)
	 	{
	 		integerPart = vr.substr(0, vr.length - decimalPlaces);
	 		decimalPart = decimalSeparator + vr.substr(vr.length - decimalPlaces, vr.length); 
	 	}
	 	

	 	field.value = "";
	 	
	 	for (var i = integerPart.length - 1; i >= 0; i--)
	 	{
	 		groupCounter++;
	 		
	 		if (groupCounter > groupSize)
	 		{
	 			field.value = integerPart.charAt(i) + "." + field.value;
	 			groupCounter = 1;
	 		}
	 		else
	 		{
	 			field.value = integerPart.charAt(i) + field.value;
	 		}
	 	}
	 	
	 	field.value += decimalPart;
	}
}