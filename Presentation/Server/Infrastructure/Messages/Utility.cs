﻿namespace Server.Infrastructure.Messages;


public static class Utility
{
	static Utility()
	{
	}

	public static bool AddMessage
		(Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary tempData,
		MessageType type, string? message)
	{
		message =
			Framework.DataType.String.Fix(message);

		if (message == null)
		{
			return false;
		}

		System.Collections.Generic.List<string>? list;

		var tempDataItems =
			(tempData[key: type.ToString()] as
			System.Collections.Generic.IList<string>);

		if (tempDataItems == null)
		{
			list = new System.Collections.Generic.List<string>();
		}
		else
		{
			list =
				tempDataItems as
				System.Collections.Generic.List<string>;

			if (list == null)
			{
				list = tempDataItems.ToList();
			}
		}

		tempData[key: type.ToString()] = list;
		// **************************************************

		if (list.Contains(item: message))
		{
			return false;
		}

		list.Add(item: message);

		return true;
	}
}
