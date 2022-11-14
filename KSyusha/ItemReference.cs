/*
 * Created by SharpDevelop.
 * User: User
 * Date: 15.05.2022
 * Time: 15:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Mono.Cecil;

namespace KSyusha
{
	/// <summary>
	/// Description of ItemReference.
	/// </summary>
	public class ItemReference
	{
		public TypeReference ItemType;
		public MemberReference Value;
		
		public string Name {
			get {
				return Value.Name;
			}
		}
		
		public ItemReference(MemberReference member, TypeReference type)
		{
			Value = member;
			ItemType = type;
		}
	}
}
