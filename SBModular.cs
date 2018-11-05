using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(SBInputs))]
public class SBModular : MonoBehaviour {

	public enum SBStates {normal, fight, dash, sprint, jump, impact, dodge, run_stop};
	public SBStates current_state = SBStates.normal; 


	Vector2 player_direction; 
	Vector2 player_cam_direction; 

	SBInputs inputs; 
	SBMove move; 
	// NierFight fight; 
	Rigidbody rb; 
	Animator anim; 


	float camera_change_state_cooldown; 


	// Use this for initialization
	void Start () {

		Initialization(); 
		
	}
	
	// Update is called once per frame
	void Update () {

		RelativeToMove(); 
		// RelativeToFight(); 
		// UpdateTimers(); 
		
	}

	// void UpdateTimers()
	// {
	// 	camera_change_state_cooldown = camera_change_state_cooldown <= 0f ? camera_change_state_cooldown : camera_change_state_cooldown - Time.deltaTime; 
	// }

	void RelativeToMove()
	{
		player_direction = inputs.GetDirection(); 
		player_cam_direction = inputs.GetCamDirection(); 

		if(Input.GetKeyDown(KeyCode.Space))
		{
			inputs.Dash = true;
			player_direction = transform.right;  
		}

		move.PlayerMove(player_direction); 

		if(inputs.Dash)
			move.Dash(); 

		if(inputs.Jump)
			move.Jump(); 

		// if(inputs.Shoot)
		// 	fight.Shoot(); 
	}

	// void RelativeToFight()
	// {
	// 	if(inputs.ChangeState && camera_change_state_cooldown <= 0f)
	// 	{
	// 		fight.ChangeState();
	// 		camera_change_state_cooldown = 0.5f;  
	// 	}

	// 	if(inputs.Hit)
	// 	{
	// 		if(current_state == SBStates.fight)
	// 			fight.Hit();
	// 		else
	// 			fight.StartCombo();  
	// 	}

	// 	if(inputs.HeavyHit)
	// 	{
	// 		if(current_state == SBStates.fight)
	// 			fight.Hit(); 
	// 		else
	// 			fight.StartHeavyCombo(); 
	// 	}

	// 	fight.ChangeTarget(player_cam_direction.x); 
	// }

	public bool IsJumping()
	{
		return current_state == SBStates.jump; 
	}

	public bool IsNormal()
	{
		return current_state == SBStates.normal; 
	}

	public bool IsFighting()
	{
		return	current_state == SBStates.fight; 
	}

	public bool IsDashing()
	{
		return current_state == SBStates.dash; 
	}

	public bool IsSprinting()
	{
		return current_state == SBStates.sprint; 
	}

	public bool IsImpacted()
	{
		return current_state == SBStates.impact; 
	}

	public bool IsDodging()
	{
		return current_state == SBStates.dodge; 
	}


	public void Inform(string info, bool state)
	{

		if(info == "Move")
		{
			if(state)
			{
				current_state = SBStates.normal; 
			}
		}

		// else if(info == "FightMode")
		// {
		// 	if(state)
		// 	{
		// 		current_state = SBStates.fight; 
		// 		move.EnterFight(); 
		// 	}
		// }

		// else if(info == "Dash")
		// {
		// 	if(state)
		// 	{
		// 		current_state = SBStates.dash; 
		// 		move.EnterDash(); 
		// 	}
		// }

		// else if(info == "Sprint")
		// {
		// 	if(state)
		// 		current_state = SBStates.sprint; 
		// 	else
		// 		move.SetWasSprinting(true); 
		// }

		// else if(info == "Dodge")
		// {
		// 	if(state)
		// 	{
		// 		fight.StopEnnemyTime(); 
		// 		current_state = SBStates.dodge; // Makes NierMove push the character transform
		// 		move.EnterDodge(); 
		// 	}
		// 	else
		// 	{
		// 		fight.ResetTime(); 
		// 	}
		// }
		else if(info == "Jump")
		{
			if(state)
			{
				move.JumpAction(); 
				move.EnterJump(); 
				current_state = SBStates.jump; 
			}
		}
		else if(info == "Drop")
		{
			if(state)
			{
				move.EnterDrop(); 
				current_state = SBStates.jump; 
			}
		}
		// else if(info == "RunStop")
		// {
		// 	if(state)
		// 	{
		// 		move.EnterStop(); 
		// 		current_state = SBStates.run_stop; 
		// 	}
		// }
		// else if(info == "Impact")
		// {
		// 	if(state)
		// 	{
		// 		move.EnterImpact(); 
		// 		current_state = SBStates.impact; 
		// 	}
		// }
		// else if(info == "Landed")
		// {
		// 	float a = 0f; 
		// }

	}

	public bool DodgeInform()
	{
		if(inputs.Dodge)
			return true; 
		else
			return false; 
	}

	// public void HitInform(NierHitData data, bool state)
	// {
	// 	fight.Activation(data, state); 
	// 	if(state)
	// 	{
	// 		move.HitImpulsion(data); 
	// 	}
	// 	return; 
	// }

	// public void WeaponInform(string state)
	// {
	// 	fight.GetWeapon(state); 
	// }

	// public void ImpactInform(NierHitData data, Vector3 direction)
	// {
	// 	bool dodge = DodgeInform(); 

	// 	if(dodge)
	// 	{
	// 		fight.Dodge(); 
	// 	}
	// 	else
	// 	{
	// 		fight.Impacted(); // triggers impact animation 
	// 		move.ChangeVelocity(direction*data.HitForce);  
	// 	}
	// }


	// This function is used to shortcut the hitbox activation in the case of a projectile using particles 

	// public void ProjectileImpactInform()
	// {
	// 	bool dodge = DodgeInform(); 
	// 	if(dodge)
	// 	{
	// 		fight.Dodge(); 
	// 	}
	// 	else
	// 	{
	// 		fight.Impacted(); 
	// 	}
	// }

	public bool Ask(string info)
	{
		if(info == "Jump")
		{
			return (current_state != SBStates.jump);
		}

		else
			return false; 
	}

	public void ComputeDashDirection()
	{
		move.ComputeDirectionAndAdjustAnim(player_direction); 
	}

	void Initialization()
	{
		inputs = GetComponent<SBInputs>(); 
		move = GetComponent<SBMove>(); 
		rb = GetComponent<Rigidbody>(); 
		anim = GetComponent<Animator>(); 
		// fight = GetComponent<NierFight>(); 
	}
}
