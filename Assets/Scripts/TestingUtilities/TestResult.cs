// from https://codinghelmet.com/articles/testing-equals-and-gethashcode
internal struct TestResult
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; set; }

	public static TestResult CreateSuccess()
	{
		return new TestResult()
		{
			IsSuccess = true
		};
	}

	public static TestResult CreateFailure(string message)
	{
		return new TestResult()
		{
			IsSuccess = false,
			ErrorMessage = message
		};
	}

}