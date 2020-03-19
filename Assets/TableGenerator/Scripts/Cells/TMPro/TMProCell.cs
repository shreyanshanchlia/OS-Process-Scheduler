using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnityUITable
{

	public class TMProCell : StyleableTableCell<TMProCellStyle>
	{

		public override bool IsCompatibleWithMember(MemberInfo member)
		{
			return member.MemberType == MemberTypes.Property || member.MemberType == MemberTypes.Field;
		}

		public override int GetPriority(MemberInfo member)
		{
			if (member.MemberType == MemberTypes.Field && ((FieldInfo)member).FieldType == typeof(string)
			|| member.MemberType == MemberTypes.Property && ((PropertyInfo)member).PropertyType == typeof(string))
				return 10;
			return base.GetPriority(member);
		}

		public TextMeshProUGUI label;

		public override void UpdateContent()
		{
			if (property == null || property.IsEmpty)
				return;
			object o = property.GetValue(obj);
			if (o == null)
				label.text = "null";
			else
				label.text = o.ToString();
		}

	}

}
