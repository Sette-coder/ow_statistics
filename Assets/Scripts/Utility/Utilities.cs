/*
 *   Copyright (c) 2020 Settimo Andrea
 *   All rights reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Tha7.Data;

namespace Tha7.Utility
{
    #region Actions Time based

    /// <summary>
    /// A collection of Coroutines to manage action called over time
    /// </summary>
    public static class ActionOverTime
    {
        #region Action<Float>

        /// <summary>
        /// Calls an Action<float> over time
        /// </summary>
        /// <param name="action">The Action<float> which usually contains a lerp</param>
        /// <param name="from">Start float value</param>
        /// <param name="to">End float value</param>
        /// <param name="duration">The duration of the operation</param>
        /// <returns></returns>
        public static IEnumerator DoActionOverTime(Action<float> action, float from, float to, float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                float value = Mathf.Lerp(from, to, time);
                action(value);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Calls an Action<float> over time
        /// </summary>
        /// <param name="action">The Action<float> which usually contains a lerp</param>
        /// <param name="from">Start float value</param>
        /// <param name="to">End float value</param>
        /// <param name="duration">The duration of the operation</param>
        /// <param name="atCompleteEvent">An Action which will be called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator DoActionOverTime(Action<float> action, float from, float to, float duration,
            Action atCompleteEvent)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                float value = Mathf.Lerp(from, to, time);
                action(value);
                yield return null;
            }

            atCompleteEvent?.Invoke();
            yield return null;
        }

        #endregion

        /// <summary>
        /// Method to manage the transform of an object over time with 3 animation curves
        /// </summary>
        /// <param name="action">The action (lerp) who wants 3 float as argoument</param>
        /// <param name="curves">The curves to define the behaviour over time</param>
        /// <param name="speed">Multiplier of the time of the animation curve</param>
        /// <returns></returns>
        public static IEnumerator DoActionOverTime(Action<float, float, float> action, FVector3Curves curves,
            float speed)
        {
            float time = 0.0f;
            float endTime = curves.ValueX.keys[curves.ValueX.length - 1].time;
            while (time <= endTime)
            {
                time += Time.deltaTime / speed;
                float valueX = curves.ValueX.Evaluate(time);
                float valueY = curves.ValueY.Evaluate(time);
                float valueZ = curves.ValueZ.Evaluate(time);
                action(valueX, valueY, valueZ);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Method to manage the transform of an object over time with 4 animation curves
        /// </summary>
        /// <param name="action">The action (lerp) who wants 3 float as argoument</param>
        /// <param name="curves">The curves to define the behaviour over time</param>
        /// <param name="speed">Multiplier of the time of the animation curve</param>
        /// <returns></returns>
        public static IEnumerator DoActionOverTime(Action<float, float, float, float> action, FVector4Curves curves,
            float speed)
        {
            float time = 0.0f;
            float endTime = curves.ValueX.keys[curves.ValueX.length - 1].time;
            while (time <= endTime)
            {
                time += Time.deltaTime / speed;
                float valueX = curves.ValueX.Evaluate(time);
                float valueY = curves.ValueY.Evaluate(time);
                float valueZ = curves.ValueZ.Evaluate(time);
                float valueW = curves.ValueW.Evaluate(time);
                action(valueX, valueY, valueZ, valueW);
                yield return null;
            }

            yield return null;
        }

        #region Action<Vector3>

        /// <summary>
        /// Calls an Action<Vector3> over time
        /// </summary>
        /// <param name="action">The Action<Vector3> which usually contains a lerp</param>
        /// <param name="from">Start Vector3 value</param>
        /// <param name="to">End Vector3 value</param>
        /// <param name="duration">The duration of the operation</param>
        /// <returns></returns>
        public static IEnumerator DoActionOverTime(Action<Vector3> action, Vector3 from, Vector3 to, float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                UnityEngine.Vector3 value = UnityEngine.Vector3.Lerp(from, to, time);
                action(value);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Calls an Action<Vector3> over time
        /// </summary>
        /// <param name="action">The Action<Vector3> which usually contains a lerp</param>
        /// <param name="from">Start Vector3 value</param>
        /// <param name="to">End Vector3 value</param>
        /// <param name="duration">The duration of the operation</param>
        /// <param name="atCompleteEvent">An Action which will be called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator DoActionOverTime(Action<Vector3> action, Vector3 from, Vector3 to, float duration,
            Action atCompleteEvent)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                UnityEngine.Vector3 value = UnityEngine.Vector3.Lerp(from, to, time);
                action(value);
                yield return null;
            }

            atCompleteEvent?.Invoke();
            yield return null;
        }

        #endregion

        #region Lerp Position & Rotation

        #region 2D

        /// <summary>
        /// Change an object WORLD position with a Vector3.lerp
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startPos">Start position</param>
        /// <param name="endPos">End position</param>
        /// <param name="duration">The operation Duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPosition2DOverTime(RectTransform objectToMove, Vector2 startPos,
            Vector2 endPos, float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.anchoredPosition = UnityEngine.Vector2.Lerp(startPos, endPos, time);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position with a Vector3.lerp
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startPos">Start position</param>
        /// <param name="endPos">End position</param>
        /// <param name="duration">The operation Duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPosition2DOverTime(RectTransform objectToMove, Vector2 startPos,
            Vector2 endPos, float duration, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.anchoredPosition = UnityEngine.Vector2.Lerp(startPos, endPos, time);
                yield return null;
            }

            afterLerpAction?.Invoke();

            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position with a Vector3.lerp
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startSize">Start position</param>
        /// <param name="endSize">End position</param>
        /// <param name="duration">The operation Duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectSizeDelta2DOverTime(RectTransform objectToMove, Vector2 startSize,
            Vector2 endSize, float duration, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.sizeDelta = Vector2.Lerp(startSize, endSize, time);
                yield return null;
            }

            afterLerpAction?.Invoke();

            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position with a Vector3.lerp
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startSize">Start position</param>
        /// <param name="endSize">End position</param>
        /// <param name="duration">The operation Duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectSizeDelta2DOverTime(RectTransform objectToMove, Vector2 startSize,
            Vector2 endSize, float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.sizeDelta = Vector2.Lerp(startSize, endSize, time);
                yield return null;
            }

            yield return null;
        }

        #endregion

        /// <summary>
        /// Change an object WORLD position with a Vector3.lerp
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startPos">Start position</param>
        /// <param name="endPos">End position</param>
        /// <param name="duration">The operation Duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPositionOverTime(Transform objectToMove, Vector3 startPos, Vector3 endPos,
            float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos, endPos, time);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position with a Vector3.lerp
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startPos">Start position</param>
        /// <param name="endPos">End position</param>
        /// <param name="duration">The operation Duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPositionOverTimeWithCurve(Transform objectToMove, Vector3 startPos,
            Vector3 endPos, float duration, AnimationCurve curveToEvaluate, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                float evaluateTime = curveToEvaluate.Evaluate(time);
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos, endPos, evaluateTime);
                yield return null;
            }

            afterLerpAction?.Invoke();
            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position with a Vector3.lerp with an UnityAction called at the end of the operation
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startPos">Start position</param>
        /// <param name="endPos">End position</param>
        /// <param name="duration">The operation Duration</param>
        /// <param name="afterLerpAction">The UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectWorldPositionOverTime(Transform objectToMove, Vector3 startPos,
            Vector3 endPos, float duration, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos, endPos, time);
                yield return null;
            }

            afterLerpAction?.Invoke();
            yield return null;
        }

        /// <summary>
        /// Change an object LOCAL position with a Vector3.lerp with an UnityAction called at the end of the operation
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startPos">Start position</param>
        /// <param name="endPos">End position</param>
        /// <param name="duration">The operation Duration</param>
        /// <param name="afterLerpAction">The UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectLocalPositionOverTime(Transform objectToMove, Vector3 startPos,
            Vector3 endPos, float duration, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.localPosition = UnityEngine.Vector3.Lerp(startPos, endPos, time);
                yield return null;
            }

            afterLerpAction?.Invoke();
            yield return null;
        }

        /// <summary>
        /// Change an object WORLD Rotation with a Quaternion.Slerp with an UnityAction called at the end of the operation
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startRot">Start rotation</param>
        /// <param name="endRot">End rotation</param>
        /// <param name="duration">The operation Duration</param>
        /// <param name="afterLerpAction">The UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectWorldRotationOverTime(Transform objectToMove, Quaternion startRot,
            Quaternion endRot, float duration, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.rotation = Quaternion.Slerp(startRot, endRot, time);
                yield return null;
            }

            afterLerpAction?.Invoke();
            yield return null;
        }

        /// <summary>
        /// Change an object LOCAL Rotation with a Quaternion.Slerp  with an UnityAction called at the end of the operation
        /// </summary>
        /// <param name="objectToMove">The Transform to move</param>
        /// <param name="startRot">Start rotation</param>
        /// <param name="endRot">End rotation</param>
        /// <param name="duration">The operation Duration</param>
        /// <param name="afterLerpAction">The UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectLocalRotationOverTime(Transform objectToMove, Quaternion startRot,
            Quaternion endRot, float duration, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.localRotation = Quaternion.Slerp(startRot, endRot, time);
                yield return null;
            }

            afterLerpAction?.Invoke();
            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position and rotation with a Vector3.lerp | Quaternion.Slerp 
        /// </summary>
        /// <param name="objectToMove">The transform to move</param>
        /// <param name="startPos">The start Transform</param>
        /// <param name="endPos">The end Transform to reach</param>
        /// <param name="duration">The operation duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPositionERotationOverTime(Transform objectToMove, Transform startPos,
            Transform endPos, float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos.position, endPos.position, time);
                objectToMove.rotation = Quaternion.Slerp(startPos.rotation, endPos.rotation, time);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position and rotation with a Vector3.lerp | Quaternion.Slerp and a UnityAction called at the end of the operation
        /// </summary>
        /// <param name="objectToMove">The transform to move</param>
        /// <param name="startPos">The start Transform</param>
        /// <param name="endPos">The end Transform to reach</param>
        /// <param name="duration">The operation duration</param>
        /// <param name="afterLerpAction">UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPositionERotationOverTime(Transform objectToMove, Transform startPos,
            Transform endPos, float duration, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos.position, endPos.position, time);
                objectToMove.rotation = Quaternion.Slerp(startPos.rotation, endPos.rotation, time);
                yield return null;
            }

            afterLerpAction.Invoke();
            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position and rotation with a Vector3.lerp | Quaternion.Slerp 
        /// </summary>
        /// <param name="objectToMove">Transform to move</param>
        /// <param name="startPos">Initial position as Transform</param>
        /// <param name="endPos">End position as Vector3</param>
        /// <param name="endRot">End rotation as Quaternion</param>
        /// <param name="duration">Float defines the operation duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPositionERotationOverTime(Transform objectToMove, Transform startPos,
            Vector3 endPos, Quaternion endRot, float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos.position, endPos, time);
                objectToMove.rotation = Quaternion.Slerp(startPos.rotation, endRot, time);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position and rotation with a Vector3.lerp | Quaternion.Slerp and a UnityAction called at the end of the operation
        /// </summary>
        /// <param name="objectToMove">Transform to move</param>
        /// <param name="startPos">Initial position as Transform</param>
        /// <param name="endPos">End position as Vector3</param>
        /// <param name="endRot">End rotation as Quaternion</param>
        /// <param name="duration">Float defines the operation duration</param>
        /// <param name="afterLerpAction">UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPositionERotationOverTime(Transform objectToMove, Transform startPos,
            Vector3 endPos, Quaternion endRot, float duration, UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos.position, endPos, time);
                objectToMove.rotation = Quaternion.Slerp(startPos.rotation, endRot, time);
                yield return null;
            }

            afterLerpAction.Invoke();
            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position and rotation with a Vector3.lerp | Quaternion.Slerp 
        /// </summary>
        /// <param name="objectToMove">Transform to move</param>
        /// <param name="startPos">Initial position as Vector3</param>
        /// <param name="startRot">Initial rotation as Quaternion</param>
        /// <param name="endPos">End position as Vector3</param>
        /// <param name="endRot">End rotation as Quaternion</param>
        /// <param name="duration">Float defines the operation duration</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPositionERotationOverTime(Transform objectToMove, Vector3 startPos,
            Quaternion startRot, Vector3 endPos, Quaternion endRot, float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos, endPos, time);
                objectToMove.rotation = Quaternion.Slerp(startRot, endRot, time);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Change an object WORLD position and rotation with a Vector3.lerp | Quaternion.Slerp With an action at the end of the process
        /// </summary>
        /// <param name="objectToMove">Transform to move</param>
        /// <param name="startPos">Initial WORLD position as Vector3</param>
        /// <param name="startRot">Initial WORLD rotation as Quaternion</param>
        /// <param name="endPos">End WORLD position as Vector3</param>
        /// <param name="endRot">End WORLD rotation as Quaternion</param>
        /// <param name="duration">Float defines the operation duration</param>
        /// <param name="afterLerpAction">UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LerpObjectPositionERotationOverTime(Transform objectToMove, Vector3 startPos,
            Quaternion startRot, Vector3 endPos, Quaternion endRot, float duration,
            UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.position = UnityEngine.Vector3.Lerp(startPos, endPos, time);
                objectToMove.rotation = Quaternion.Slerp(startRot, endRot, time);
                yield return null;
            }

            afterLerpAction?.Invoke();
            yield return null;
        }


        /// <summary>
        /// Change an object LOCAL position and rotation with a Vector3.lerp | Quaternion.Slerp With an action at the end of the process
        /// </summary>
        /// <param name="objectToMove">Transform to move</param>
        /// <param name="startPos">Initial LOCAL position as Vector3</param>
        /// <param name="startRot">Initial LOCAL rotation as Quaternion</param>
        /// <param name="endPos">End LOCAL position as Vector3</param>
        /// <param name="endRot">End LOCAL rotation as Quaternion</param>
        /// <param name="duration">Float defines the operation duration</param>
        /// <param name="afterLerpAction">UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LerpLocalObjectPositionERotationOverTime(Transform objectToMove, Vector3 startPos,
            Quaternion startRot, Vector3 endPos, Quaternion endRot, float duration,
            UnityAction afterLerpAction)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToMove.localPosition = UnityEngine.Vector3.Lerp(startPos, endPos, time);
                objectToMove.localRotation = Quaternion.Slerp(startRot, endRot, time);
                yield return null;
            }

            afterLerpAction?.Invoke();
            yield return null;
        }

        /// <summary>
        /// Linear interpolation of the local scale of a given transform 
        /// </summary>
        /// <param name="objectToScale">The transform to change the scale</param>
        /// <param name="startScale">Initial scale value as Vector3</param>
        /// <param name="endScale">End scale value as Vector3</param>
        /// <param name="duration">Float defines the operation duration</param>
        /// <returns></returns>
        public static IEnumerator LinearLerpObjectScaleOverTime(Transform objectToScale, Vector3 startScale,
            Vector3 endScale, float duration)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToScale.localScale = UnityEngine.Vector3.Lerp(startScale, endScale, time);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Linear interpolation of the local scale of a given transform with an UnityAction called at the end of the operation
        /// </summary>
        /// <param name="objectToScale">The transform to change the scale</param>
        /// <param name="startScale">Initial scale value as Vector3</param>
        /// <param name="endScale">End scale value as Vector3</param>
        /// <param name="duration">Float defines the operation duration</param>
        /// <param name="actionAfterLerp">UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator LinearLerpObjectScaleOverTime(Transform objectToScale, Vector3 startScale,
            Vector3 endScale, float duration, UnityAction actionAfterLerp)
        {
            float time = 0.0f;
            while (time <= 1.0f)
            {
                time += Time.deltaTime / duration;
                objectToScale.localScale = UnityEngine.Vector3.Lerp(startScale, endScale, time);
                yield return null;
            }

            actionAfterLerp.Invoke();
            yield return null;
        }

        /// <summary>
        /// Linear interpolation with bump of the local scale of a given transform
        /// </summary>
        /// <param name="objectToScale">The transform to change</param>
        /// <param name="startScale">Initial scale value as Vector3</param>
        /// <param name="endScale">End scale value as Vector3</param>
        /// <param name="maximumScaleBump">The scale value obtained at the percentage given</param>
        /// <param name="bumpTimePosition">The value of the normalized time when the scale should be the bumped value (0-1)</param>
        /// <param name="duration">The duration of the operation</param>
        /// <returns></returns>
        public static IEnumerator BumpedLerpObjectScaleOverTime(Transform objectToScale, Vector3 startScale,
            Vector3 endScale, Vector3 maximumScaleBump, float bumpTimePosition, float duration)
        {
            float time = 0.0f;

            while (time <= 1.0f)
            {
                time += Time.deltaTime / (duration * bumpTimePosition);
                objectToScale.localScale = UnityEngine.Vector3.Lerp(startScale, maximumScaleBump, time);
                yield return null;
            }

            time = 0.0f;

            while (time <= 1.0f)
            {
                time += Time.deltaTime / (duration * (1 - bumpTimePosition));
                objectToScale.localScale = UnityEngine.Vector3.Lerp(maximumScaleBump, endScale, time);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Linear interpolation with bump of the local scale of a given transform with an UnityAction called at the end of the operation
        /// </summary>
        /// <param name="objectToScale">The transform to change</param>
        /// <param name="startScale">Initial scale value as Vector3</param>
        /// <param name="endScale">End scale value as Vector3</param>
        /// <param name="maximumScaleBump">The scale value obtained at the percentage given</param>
        /// <param name="bumpTimePosition">The value of the normalized time when the scale should be the bumped value (0-1) </param>
        /// <param name="duration">The duration of the operation</param>
        /// <param name="actionAfterLerp">UnityAction called at the end of the operation</param>
        /// <returns></returns>
        public static IEnumerator BumpedLerpObjectScaleOverTime(Transform objectToScale, Vector3 startScale,
            Vector3 endScale,
            Vector3 maximumScaleBump, float bumpTimePosition, float duration, UnityAction actionAfterLerp)
        {
            float time = 0.0f;

            while (time <= 1.0f)
            {
                time += Time.deltaTime / (duration * bumpTimePosition);
                objectToScale.localScale = UnityEngine.Vector3.Lerp(startScale, maximumScaleBump, time);
                yield return null;
            }

            time = 0.0f;

            while (time <= 1.0f)
            {
                time += Time.deltaTime / (duration * (1 - bumpTimePosition));
                objectToScale.localScale = UnityEngine.Vector3.Lerp(maximumScaleBump, endScale, time);
                yield return null;
            }

            actionAfterLerp.Invoke();
            yield return null;
        }

        #endregion
    }

    /// <summary>
    /// A collection of Coroutines to manage action with a time delay from call
    /// </summary>
    public static class ActionAfterTimer
    {
        /// <summary>
        /// Calls an UnityAction after an amount of time given
        /// </summary>
        /// <param name="timeToWait">How much time to wait</param>
        /// <param name="action">The Unity Action to Call after the waiting</param>
        /// <returns></returns>
        public static IEnumerator DoActionAfterWaiting(float timeToWait, UnityAction action)
        {
            yield return new WaitForSeconds(timeToWait);
            action?.Invoke();
        }

        /// <summary>
        /// Calls an UnityAction with a generic Class as argument after an amount of time
        /// </summary>
        /// <typeparam name="T">A class Type</typeparam>
        /// <param name="timeToWait">Float defines how much time to wait</param>
        /// <param name="argument">The argument to give in the action called</param>
        /// <param name="action">The UnityAction<T> To call after the waiting</param>
        /// <returns></returns>
        public static IEnumerator DoActionAfterWaiting<T>(float timeToWait, T argument, UnityAction<T> action)
            where T : class
        {
            yield return new WaitForSeconds(timeToWait);
            action?.Invoke(argument);
        }


        /// <summary>
        /// Calls an EventHandler with a generic Class as argument after an amount of time
        /// </summary>
        /// <typeparam name="T">A class Type</typeparam>
        /// <param name="timeToWait">Float defines how much time to wait</param>
        /// <param name="sender">The object to define the sender for the EventHandler</param>
        /// <param name="argument">The argument to give in the action called</param>
        /// <param name="action">The EventHandler<T> To call after the waiting</param>
        /// <returns></returns>
        public static IEnumerator DoActionAfterWaiting<T>(float timeToWait, object sender, T argument,
            EventHandler<T> action) where T : class
        {
            yield return new WaitForSeconds(timeToWait);
            action?.Invoke(sender, argument);
        }

        /// <summary>
        /// Calls an UnityEvent after an amount of time given
        /// </summary>
        /// <param name="timeToWait">How much time to wait</param>
        /// <param name="action">The Unity Event to Call after the waiting</param>
        /// <returns></returns>
        public static IEnumerator DoActionAfterWaiting(float timeToWait, UnityEvent action)
        {
            yield return new WaitForSeconds(timeToWait);
            action?.Invoke();
        }

        /// <summary>
        /// Calls an UnityAction at the end of the frame
        /// </summary>
        /// <param name="action">The UnityAction to call at the end of the frame</param>
        /// <returns></returns>
        public static IEnumerator DoActionAtTheEndOfTheFrame(UnityAction action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }

    #endregion

    /// <summary>
    /// A collection of Async operations
    /// </summary>
    public static class AsyncOperations
    {
        public static IEnumerator LoadAdditiveSceneAsync(string sceneName, bool allowSceneActivation,
            UnityAction<AsyncOperation> fakeReturn, UnityAction onSceneLoadedComplete)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            asyncOperation.allowSceneActivation = allowSceneActivation;

            fakeReturn?.Invoke(asyncOperation);
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    onSceneLoadedComplete?.Invoke();
                }

                yield return null;
            }


            yield return asyncOperation;
        }

        public static IEnumerator UnloadSceneAsync(string sceneName, UnityAction<AsyncOperation> fakeReturn,
            UnityAction onSceneUnloadedComplete)
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            fakeReturn?.Invoke(asyncOperation);

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    onSceneUnloadedComplete?.Invoke();
                }

                yield return null;
            }

            yield return asyncOperation;
        }
    }
}

namespace Tha7.Utility.Math
{
    public static class RemapClass
    {
        /// <summary>
        /// Remap class to Change a value from a start\end Value to another range
        /// </summary>
        /// <param name="value">The float who has to change</param>
        /// <param name="from1">Initial Value</param>
        /// <param name="to1">End Value</param>
        /// <param name="from2">New minimum Value</param>
        /// <param name="to2">New maximum value</param>
        /// <returns></returns>
        public static float Remap(this float value, float from1, float to1, float from2, float to2) =>
            ((value - from1) / (to1 - from1) * (to2 - from2) + from2);

        /// <summary>
        /// Remap class to Change a value from a start\end Value to 0-1 range
        /// </summary>
        /// <param name="value">The float who has to change</param>
        /// <param name="from1">Initial Value</param>
        /// <param name="to1">End Value</param>
        /// <returns></returns>
        public static float Normalize(this float value, float from1, float to1) =>
            ((value - from1) / (to1 - from1) * (1 - 0) + 0);
    }

    public static class ManualParse
    {
        public static char[] chars;
        public static List<float> values = new List<float>();

        /// <summary>
        /// returns a float from a given string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ManualParseMethod(string s)
        {
            values = new List<float>();
            chars = s.ToCharArray();

            string stringMinus = "";

            if (s.Contains("-")) stringMinus = "-";


            for (int i = 0; i < chars.Length; i++)
            {
                if (int.TryParse(chars[i].ToString(), out int parseResult))
                {
                    values.Add(parseResult);
                }
                else if
                    (chars[i] == ',' || chars[i] == '.')
                {
                    values.Add(777);
                }
                else
                    continue;
            }


            float result = 0;

            bool previusDot = false;

            int post = 1;

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == 777)
                    previusDot = true;
                else if (previusDot)
                {
                    float multi = Mathf.Pow(10, post);
                    post++;
                    result += (values[i] / multi);
                }
                else
                {
                    result *= 10;
                    result += values[i];
                }
            }

            if (!string.IsNullOrEmpty(stringMinus)) result = result * -1;

            return result;
        }
    }

    public static class ManualCeilToInt
    {
        /// <summary>
        /// returns an int calculated in excess with any number greater than 0 after the comma 
        /// </summary>
        /// <param name="floatToParse"></param>
        /// <returns>  only a number after the comma is recognized </returns>
        public static int CustomCeilToInt(float floatToParse)
        {
            int valueToReturn = 0;

            char[] decomposedValue = floatToParse.ToString().Replace(",", ".").ToCharArray();

            int elementsAfterComma = 0;

            string recomposedValueString = "";

            for (int i = 0; i < decomposedValue.Length; i++)
            {
                recomposedValueString += decomposedValue[i].ToString();

                if (elementsAfterComma == 1)
                {
                    int v = (int)ManualParse.ManualParseMethod(decomposedValue[i].ToString());

                    if (v != 0) valueToReturn = (int)ManualParse.ManualParseMethod(recomposedValueString) + 1;

                    break;
                }
                else if (elementsAfterComma == 0)
                {
                    // element before comma
                    valueToReturn = (int)ManualParse.ManualParseMethod(recomposedValueString);
                }

                if (decomposedValue[i].ToString() == ".")
                {
                    // recognition comma value
                    elementsAfterComma++;
                }
            }

            return valueToReturn;
        }
    }

    public static class Vector3Approximately
    {
        /// <summary>
        /// Returns true if the two Vectors given are near less than threshold
        /// </summary>
        /// <param name="me">First Vector to compare</param>
        /// <param name="other">Second Vector to compare</param>
        /// <param name="threshold">The threshold to return true</param>
        /// <returns></returns>
        public static bool ApproximatelyWithThreshold(Vector3 me, Vector3 other, float threshold)
        {
            float dx = me.x - other.x;
            if (Mathf.Abs(dx) > threshold) return false;

            float dy = me.y - other.y;
            if (Mathf.Abs(dy) > threshold) return false;

            float dz = me.z - other.z;

            return Mathf.Abs(dz) <= threshold;
        }

        /// <summary>
        /// Returns true if the two Vectors given are near less than threshold
        /// </summary>
        /// <param name="me">First Vector to compare</param>
        /// <param name="other">Second Vector to compare</param>
        /// <param name="threshold">The threshold to return true</param>
        /// <returns></returns>
        public static bool ApproximatelyWithThreshold(Vector3 me, Vector3 other, float threshold,
            out float maxDifference)
        {
            float dx = me.x - other.x;

            maxDifference = Mathf.Abs(dx);

            if (Mathf.Abs(dx) > threshold) return false;

            float dy = me.y - other.y;

            if (Mathf.Abs(dy) > maxDifference) maxDifference = Mathf.Abs(dy);

            if (Mathf.Abs(dy) > threshold) return false;

            float dz = me.z - other.z;
            if (Mathf.Abs(dz) > maxDifference) maxDifference = Mathf.Abs(dz);

            return Mathf.Abs(dz) <= threshold;
        }

        /// <summary>
        /// Returns true if the two vectors given are near less than threshold (Threshold given in percentage of the first vector)
        /// </summary>
        /// <param name="me">First Vector to compare</param>
        /// <param name="other">Second Vector to compare</param>
        /// <param name="percentage">The threshold to return true, In percentage of the first vector (0-1)</param>
        /// <returns></returns>
        public static bool ApproximatelyWithPercentage(Vector3 me, Vector3 other, float percentage)
        {
            float dx = me.x - other.x;
            if (Mathf.Abs(dx) > me.x * percentage) return false;

            float dy = me.y - other.y;
            if (Mathf.Abs(dy) > me.y * percentage) return false;

            float dz = me.z - other.z;

            return Mathf.Abs(dz) <= me.z * percentage;
        }
    }
}

namespace Tha7.Data
{
    [Serializable]
    public struct FVector3Curves
    {
        public AnimationCurve ValueX;
        public AnimationCurve ValueY;
        public AnimationCurve ValueZ;
    }

    [Serializable]
    public struct FVector4Curves
    {
        public AnimationCurve ValueX;
        public AnimationCurve ValueY;
        public AnimationCurve ValueZ;
        public AnimationCurve ValueW;
    }
}