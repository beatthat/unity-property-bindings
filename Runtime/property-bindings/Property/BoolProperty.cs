using BeatThat.TransformPathExt;
using UnityEngine;


namespace BeatThat.Properties{
    /// <summary>
    /// A BoolProp that stores its value as a member variable
    /// </summary>
    public class BoolProperty : BoolProp
	{
		public bool m_value; // TODO: this shouldn't be public but good to see in Inspector. Move to editor class.

		[Tooltip("set FALSE if you want a param to hold its value across disable/enable")]
		public bool m_resetValueOnDisable = true;

		override protected bool GetValue() { return m_value; }

		override protected void EnsureValue(bool s) 
		{
			m_value = s;
		}

		override protected void _SetValue(bool s) { m_value = s; }


		virtual protected void OnDidApplyAnimationProperties()
		{
			#if BT_DEBUG_UNSTRIP || UNITY_EDITOR
			if(m_debug) {
				Debug.Log("[" + Time.frameCount + "][" + this.Path() + "] " + GetType() + "::OnDidApplyAnimationProperties");
			}
			#endif

			SetValue(m_value);
		}


		virtual protected void OnDisable()
		{
			if(m_resetValueOnDisable) {
				m_value = m_resetValue;
			}
		}

		override protected void Start()
		{
			base.Start ();
			this.didStart = true;
			SetValue(m_value);
		}

		private bool didStart { get; set; }

		virtual protected void OnEnable()
		{
			if(!this.didStart) {
				return;
			}

			SetValue(m_value);

			if(m_debug) {
				Debug.Log("[" + Time.frameCount + "][" + this.Path() + "] " + GetType() + "::OnEnable");
			}
		}


		void OnDestroy()
		{
			#if BT_DEBUG_UNSTRIP || UNITY_EDITOR
			if(m_debug) {
				Debug.Log("[" + Time.frameCount + "][" + this.Path() + "] " + GetType() + "::OnDestroy");
			}
			#endif
		}

	}

}

