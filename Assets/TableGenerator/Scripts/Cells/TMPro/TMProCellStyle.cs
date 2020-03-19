using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUITable
{

	public class TMProCellStyle : TableCellStyle
	{

		[Serializable]
		public struct AutoSizeOptions
		{
			public float min;
			public float max;
			public float wd;
			public float line;
		}

		[Serializable]
		public struct Margins
		{
			public Vector4 marginsV4;
		}

		[Serializable]
		public struct Spacings
		{
			public float character;
			public float word;
			public float line;
			public float paragraph;
		}
		[Serializable]
		public struct TMProFontData
		{
			public TMP_FontAsset font;
			public Material material;
		}

		[Serializable]
		public class TMProData
		{
			public TMProFontData fontData;
			public FontStyles fontStyle;
			public float fontSize;
			public bool autoSize;
			public AutoSizeOptions autoSizeOptions;
			public Color color;
			public bool overrideTags;
			public Spacings spacings;
			public TextAlignmentOptions alignment;
			public bool wrapping;
			public TextOverflowModes overflow;
			public TextureMappingOptions horizontalMapping;
			public TextureMappingOptions verticalMapping;
			public Margins margins;
			public VertexSortingOrder geometrySorting;
			public bool richText;
			public bool raycastTarget;
			public bool parseEscapeCharaters;
			public bool visibleDescender;
			public TMP_SpriteAsset spriteAsset;
			public bool kerning;
			public bool extraPadding;

			public TMProData(TextMeshProUGUI text)
			{
				this.fontData.font = text.font;
				this.fontData.material = text.fontSharedMaterial;
				this.fontStyle = text.fontStyle;
				this.fontSize = text.fontSize;
				this.autoSize = text.enableAutoSizing;
				this.autoSizeOptions.min = text.fontSizeMin;
				this.autoSizeOptions.max = text.fontSizeMax;
				this.autoSizeOptions.wd = text.characterWidthAdjustment;
				this.autoSizeOptions.line = text.lineSpacingAdjustment;
				this.color = text.color;
				this.overrideTags = text.overrideColorTags;
				this.spacings.character = text.characterSpacing;
				this.spacings.word = text.wordSpacing;
				this.spacings.line = text.lineSpacing;
				this.spacings.paragraph = text.paragraphSpacing;
				this.alignment = text.alignment;
				this.wrapping = text.enableWordWrapping;
				this.overflow = text.overflowMode;
				this.horizontalMapping = text.horizontalMapping;
				this.verticalMapping = text.verticalMapping;
				this.margins.marginsV4 = text.margin;
				this.geometrySorting = text.geometrySortingOrder;
				this.richText = text.richText;
				this.raycastTarget = text.raycastTarget;
				this.parseEscapeCharaters = text.parseCtrlCharacters;
				this.visibleDescender = text.useMaxVisibleDescender;
				this.spriteAsset = text.spriteAsset;
				this.kerning = text.enableKerning;
				this.extraPadding = text.extraPadding;
			}
		}

		[Serializable]
		public class TMProDataSetting : CellSpecificSetting<TMProData, TMProCell>
		{
			public TMProDataSetting(Action<TMProCell, TMProData> applySetting, Func<TMProCell, TMProData> defaultValueGetter) : base(applySetting, defaultValueGetter) { }
		}

		public TMProDataSetting fontData = new TMProDataSetting(
			(cell, v) =>
			{
				TextMeshProUGUI text = cell.label;
				text.font = v.fontData.font;
				text.fontSharedMaterial = v.fontData.material;
				text.fontStyle = v.fontStyle;
				text.fontSize = v.fontSize;
				text.enableAutoSizing = v.autoSize;
				text.fontSizeMin = v.autoSizeOptions.min;
				text.fontSizeMax = v.autoSizeOptions.max;
				text.characterWidthAdjustment = v.autoSizeOptions.wd;
				text.lineSpacingAdjustment = v.autoSizeOptions.line;
				text.color = v.color;
				text.overrideColorTags = v.overrideTags;
				text.characterSpacing = v.spacings.character;
				text.wordSpacing = v.spacings.word;
				text.lineSpacing = v.spacings.line;
				text.paragraphSpacing = v.spacings.paragraph;
				text.alignment = v.alignment;
				text.enableWordWrapping = v.wrapping;
				text.overflowMode = v.overflow;
				text.horizontalMapping = v.horizontalMapping;
				text.verticalMapping = v.verticalMapping;
				text.margin = v.margins.marginsV4;
				text.geometrySortingOrder = v.geometrySorting;
				text.richText = v.richText;
				text.raycastTarget = v.raycastTarget;
				text.parseCtrlCharacters = v.parseEscapeCharaters;
				text.useMaxVisibleDescender = v.visibleDescender;
				text.spriteAsset = v.spriteAsset;
				text.enableKerning = v.kerning;
				text.extraPadding = v.extraPadding;
			},
			(cell =>
			{
				TextMeshProUGUI text = cell.label;
				TMProData res = new TMProData(text);
				return res;
			}));

	}


#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(TMProCellStyle.TMProDataSetting), true)]
	public class TMProDataSettingDrawer : PropertyDrawer
	{

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), new GUIContent("Data"), true);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"), new GUIContent("Data"), true);
		}

	}

	[CustomPropertyDrawer(typeof(TMProCellStyle.Margins), true)]
	public class MarginsDrawer : PropertyDrawer
	{

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			property = property.FindPropertyRelative("marginsV4");

			Rect rect = position;

			EditorGUI.BeginProperty(rect, label, property);

			Rect pos0 = new Rect(rect.x + 15, rect.y + 2, rect.width - 15, 18);

			float width = rect.width + 3;
			pos0.width = EditorGUIUtility.labelWidth;
			GUI.Label(pos0, label);

			var vec = property.vector4Value;

			float widthB = width - EditorGUIUtility.labelWidth;
			float fieldWidth = widthB / 4;
			pos0.width = Mathf.Max(fieldWidth - 5, 45f);

			// Labels
			pos0.x = EditorGUIUtility.labelWidth + 15;
			GUI.Label(pos0, "Left");

			pos0.x += fieldWidth;
			GUI.Label(pos0, "Top");

			pos0.x += fieldWidth;
			GUI.Label(pos0, "Right");

			pos0.x += fieldWidth;
			GUI.Label(pos0, "Bottom");

			pos0.y += 18;

			pos0.x = EditorGUIUtility.labelWidth;
			vec.x = EditorGUI.FloatField(pos0, GUIContent.none, vec.x);

			pos0.x += fieldWidth;
			vec.y = EditorGUI.FloatField(pos0, GUIContent.none, vec.y);

			pos0.x += fieldWidth;
			vec.z = EditorGUI.FloatField(pos0, GUIContent.none, vec.z);

			pos0.x += fieldWidth;
			vec.w = EditorGUI.FloatField(pos0, GUIContent.none, vec.w);

			property.vector4Value = vec;

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight * 2.5f;
		}

	}
	[CustomPropertyDrawer(typeof(TMProCellStyle.Spacings), true)]
	public class SpacingsDrawer : PropertyDrawer
	{

		protected SerializedProperty m_CharacterSpacingProp;
		protected SerializedProperty m_WordSpacingProp;
		protected SerializedProperty m_LineSpacingProp;
		protected SerializedProperty m_ParagraphSpacingProp;

		static readonly GUIContent k_CharacterSpacingLabel = new GUIContent("Character");
		static readonly GUIContent k_WordSpacingLabel = new GUIContent("Word");
		static readonly GUIContent k_LineSpacingLabel = new GUIContent("Line");
		static readonly GUIContent k_ParagraphSpacingLabel = new GUIContent("Paragraph");

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			m_CharacterSpacingProp = property.FindPropertyRelative("character");
			m_WordSpacingProp = property.FindPropertyRelative("word");
			m_LineSpacingProp = property.FindPropertyRelative("line");
			m_ParagraphSpacingProp = property.FindPropertyRelative("paragraph");

			// CHARACTER, LINE & PARAGRAPH SPACING
			EditorGUI.BeginChangeCheck();

			Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

			EditorGUI.PrefixLabel(rect, new GUIContent("Spacing Options"));

			int oldIndent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			rect.x += EditorGUIUtility.labelWidth;
			rect.width = (rect.width - EditorGUIUtility.labelWidth - 3f) / 2f;

			EditorGUIUtility.labelWidth = Mathf.Min(rect.width * 0.55f, 80f);

			EditorGUI.PropertyField(rect, m_CharacterSpacingProp, k_CharacterSpacingLabel);
			rect.x += rect.width + 3f;
			EditorGUI.PropertyField(rect, m_WordSpacingProp, k_WordSpacingLabel);

			rect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight);
			EditorGUIUtility.labelWidth = 0;
			rect.x += EditorGUIUtility.labelWidth;
			rect.width = (rect.width - EditorGUIUtility.labelWidth - 3f) / 2f;
			EditorGUIUtility.labelWidth = Mathf.Min(rect.width * 0.55f, 80f);

			EditorGUI.PropertyField(rect, m_LineSpacingProp, k_LineSpacingLabel);
			rect.x += rect.width + 3f;
			EditorGUI.PropertyField(rect, m_ParagraphSpacingProp, k_ParagraphSpacingLabel);

			EditorGUIUtility.labelWidth = 0;
			EditorGUI.indentLevel = oldIndent;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight * 2.5f;
		}

	}
	[CustomPropertyDrawer(typeof(TMProCellStyle.AutoSizeOptions), true)]
	public class AutoSizeOptionsDrawer : PropertyDrawer
	{

		protected SerializedProperty m_FontSizeMinProp;
		protected SerializedProperty m_FontSizeMaxProp;
		protected SerializedProperty m_LineSpacingMaxProp;
		protected SerializedProperty m_CharWidthMaxAdjProp;

		static readonly GUIContent k_AutoSizeOptionsLabel = new GUIContent("Auto Size Options");
		static readonly GUIContent k_MinLabel = new GUIContent("Min", "The minimum font size.");
		static readonly GUIContent k_MaxLabel = new GUIContent("Max", "The maximum font size.");
		static readonly GUIContent k_WdLabel = new GUIContent("WD%", "Compresses character width up to this value before reducing font size.");
		static readonly GUIContent k_LineLabel = new GUIContent("Line", "Negative value only. Compresses line height down to this value before reducing font size.");

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			m_FontSizeMinProp = property.FindPropertyRelative("min");
			m_FontSizeMaxProp = property.FindPropertyRelative("max");
			m_LineSpacingMaxProp = property.FindPropertyRelative("line");
			m_CharWidthMaxAdjProp = property.FindPropertyRelative("wd");
			Rect rect = position;

			EditorGUI.PrefixLabel(rect, k_AutoSizeOptionsLabel);

			int previousIndent = EditorGUI.indentLevel;

			EditorGUI.indentLevel = 0;

			rect.width = (rect.width - EditorGUIUtility.labelWidth) / 4f;
			rect.x += EditorGUIUtility.labelWidth;

			EditorGUIUtility.labelWidth = 24;
			EditorGUI.BeginChangeCheck();
			EditorGUI.PropertyField(rect, m_FontSizeMinProp, k_MinLabel);
			if (EditorGUI.EndChangeCheck())
			{
				m_FontSizeMinProp.floatValue = Mathf.Min(m_FontSizeMinProp.floatValue, m_FontSizeMaxProp.floatValue);
			}
			rect.x += rect.width;

			EditorGUIUtility.labelWidth = 27;
			EditorGUI.BeginChangeCheck();
			EditorGUI.PropertyField(rect, m_FontSizeMaxProp, k_MaxLabel);
			if (EditorGUI.EndChangeCheck())
			{
				m_FontSizeMaxProp.floatValue = Mathf.Max(m_FontSizeMinProp.floatValue, m_FontSizeMaxProp.floatValue);
			}
			rect.x += rect.width;

			EditorGUI.BeginChangeCheck();
			EditorGUIUtility.labelWidth = 36;
			EditorGUI.PropertyField(rect, m_CharWidthMaxAdjProp, k_WdLabel);
			rect.x += rect.width;
			EditorGUIUtility.labelWidth = 28;
			EditorGUI.PropertyField(rect, m_LineSpacingMaxProp, k_LineLabel);

			EditorGUIUtility.labelWidth = 0;

			if (EditorGUI.EndChangeCheck())
			{
				m_CharWidthMaxAdjProp.floatValue = Mathf.Clamp(m_CharWidthMaxAdjProp.floatValue, 0, 50);
				m_LineSpacingMaxProp.floatValue = Mathf.Min(0, m_LineSpacingMaxProp.floatValue);
			}
		}

	}

	[CustomPropertyDrawer(typeof(TMProCellStyle.TMProFontData), true)]
	public class TMProFontDataDrawer : PropertyDrawer
	{

		protected SerializedProperty m_FontAssetProp;
		protected SerializedProperty m_FontSharedMaterialProp;

		protected Material[] m_MaterialPresets;
		protected GUIContent[] m_MaterialPresetNames;
		protected int m_MaterialPresetSelectionIndex;
		protected bool m_IsPresetListDirty = true;
		protected bool m_HavePropertiesChanged;

		/// <summary>
		/// Method to retrieve the material presets that match the currently selected font asset.
		/// </summary>
		protected GUIContent[] GetMaterialPresets()
		{
			TMP_FontAsset fontAsset = m_FontAssetProp.objectReferenceValue as TMP_FontAsset;
			if (fontAsset == null) return null;

			m_MaterialPresets = TMPro.EditorUtilities.TMP_EditorUtility.FindMaterialReferences(fontAsset);
			m_MaterialPresetNames = new GUIContent[m_MaterialPresets.Length];

			for (int i = 0; i < m_MaterialPresetNames.Length; i++)
			{
				m_MaterialPresetNames[i] = new GUIContent(m_MaterialPresets[i].name);

				if (m_FontSharedMaterialProp.objectReferenceValue != null && m_FontSharedMaterialProp.objectReferenceValue.GetInstanceID() == m_MaterialPresets[i].GetInstanceID())
					m_MaterialPresetSelectionIndex = i;
			}

			m_IsPresetListDirty = false;

			return m_MaterialPresetNames;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			m_FontAssetProp = property.FindPropertyRelative("font");
			m_FontSharedMaterialProp = property.FindPropertyRelative("material");

			Rect rect = position;
			rect.height = EditorGUIUtility.singleLineHeight;
			
			// Update list of material presets if needed.
			if (m_IsPresetListDirty)
				m_MaterialPresetNames = GetMaterialPresets();

			// FONT ASSET
			EditorGUI.BeginChangeCheck();
			EditorGUI.PropertyField(rect, m_FontAssetProp, new GUIContent("Font"));
			if (EditorGUI.EndChangeCheck())
			{
				m_HavePropertiesChanged = true;
				//m_HasFontAssetChangedProp.boolValue = true;

				m_IsPresetListDirty = true;
				m_MaterialPresetSelectionIndex = 0;
			}

			// MATERIAL PRESET
			if (m_MaterialPresetNames != null)
			{
				EditorGUI.BeginChangeCheck();
				rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

				float oldHeight = EditorStyles.popup.fixedHeight;
				EditorStyles.popup.fixedHeight = rect.height;

				int oldSize = EditorStyles.popup.fontSize;
				EditorStyles.popup.fontSize = 11;

				m_MaterialPresetSelectionIndex = EditorGUI.Popup(rect, new GUIContent("Material"), m_MaterialPresetSelectionIndex, m_MaterialPresetNames);
				if (EditorGUI.EndChangeCheck())
				{
					m_FontSharedMaterialProp.objectReferenceValue = m_MaterialPresets[m_MaterialPresetSelectionIndex];
					m_HavePropertiesChanged = true;
				}

				//Make sure material preset selection index matches the selection
				if (m_MaterialPresetSelectionIndex < m_MaterialPresetNames.Length && m_FontSharedMaterialProp.objectReferenceValue != m_MaterialPresets[m_MaterialPresetSelectionIndex] && !m_HavePropertiesChanged)
					m_IsPresetListDirty = true;

				EditorStyles.popup.fixedHeight = oldHeight;
				EditorStyles.popup.fontSize = oldSize;
			}

		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight * 2f + EditorGUIUtility.standardVerticalSpacing;
		}

	}
#endif

}
