using UnityEngine;
using System.Collections;

public abstract class ComponentState<Controller> 
	where Controller : Component {

	protected Controller controller;
	protected Transform transform;
	protected string stateName = "State";

	public ComponentState(Controller controller) {
		this.controller = controller;
		this.transform  = controller.transform;
	}

	public virtual void Enter() {}
	public virtual void Exit()  {}
	
	public virtual void FixedUpdate() {}
	public virtual void Update()      {}
	public virtual void LateUpdate()  {}

	public string StateName {
		get { return stateName; }
	}	
}

public abstract class StateControlledMonoBehavior<BaseState, Controller> : SafeMonoBehaviour 
  	where BaseState  : ComponentState<Controller>
    where Controller : Component {

	protected BaseState _state; 

	public delegate void Exit();
	public delegate void Enter();
	public event Enter OnEnter = delegate {};
	public event Exit  OnExit  = delegate {};

	public BaseState State {
		get { return _state; }
		
		set {
			if (_state != null) {
				_state.Exit();
				OnExit();
			}

			_state = value;
			_state.Enter();
			OnEnter();
		}
	}

	protected virtual void FixedUpdate() {
		State.FixedUpdate();
	}

	protected virtual void Update() {
		State.Update();
	}

	protected override void LateUpdate() {
		base.LateUpdate();
		State.LateUpdate();
	}
}