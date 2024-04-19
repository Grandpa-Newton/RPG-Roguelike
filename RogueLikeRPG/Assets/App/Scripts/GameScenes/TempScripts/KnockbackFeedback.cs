using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
   private Rigidbody2D _rigidbody2d;
   [SerializeField] private float strength = 16;
   [SerializeField] private float delay = 0.15f;
   
   public UnityEvent OnBegin;
   public UnityEvent OnDone;

   private void Awake()
   {
      _rigidbody2d = GetComponent<Rigidbody2D>();
   }

   public void PlayFeedBack(GameObject sender)
   {
      if (sender == null)
      {
         Debug.LogError("Sender is null");
         return;
      }
      
      StopAllCoroutines();
      OnBegin?.Invoke();

      Vector2 direction = (transform.position - sender.transform.position).normalized;
      _rigidbody2d.AddForce(direction * strength, ForceMode2D.Impulse);
      StartCoroutine(Reset());
   }
   private IEnumerator Reset()
   {
      yield return new WaitForSeconds(delay);
      _rigidbody2d.velocity = Vector3.zero;
      OnDone?.Invoke();
   }
}
