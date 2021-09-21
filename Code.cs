public class Script : ScriptBase
{
	public override async Task<HttpResponseMessage> ExecuteAsync()
	{
		return await this.HandleMathOperation().ConfigureAwait(false);
	}

	private async Task<HttpResponseMessage> HandleMathOperation()
	{
		HttpResponseMessage response;
		
        string operationId = (string)this.Context.OperationId;
		
		var contentAsString = await this.Context.Request.Content.ReadAsStringAsync().ConfigureAwait(false);

		// Parse as JSON object
		var contentAsJson = JObject.Parse(contentAsString);

		// Get the Number
		double number = (double)contentAsJson["Number"];
        
        if(operationId == "MATH_ROUND")
			number = Math.Round(number);
        else if(operationId == "MATH_CEILING")
            number = Math.Ceiling(number);
		else if(operationId == "MATH_FLOOR")
			number = Math.Floor(number);
        

		JObject output = new JObject
		{
			["value"] = (float)number,
		};

		response = new HttpResponseMessage(HttpStatusCode.OK);
		response.Content = CreateJsonContent(output.ToString());
		return response;
	}
}