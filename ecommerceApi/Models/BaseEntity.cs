using System;
namespace ecommerceApi.Models;

public class BaseEntity
{
	public Guid Gid { get; private set; }

	public BaseEntity()
	{
		Gid = Guid.NewGuid();
	}
}

