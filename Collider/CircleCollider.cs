﻿using System;
using Daze.Geometry;

namespace Daze {
    /// <summary>
    /// A collider that use a circle as it's shape... wow, so unexpected.
    /// </summary>
    public class CircleCollider:Collider {
        /// <summary>
        /// The circle that's used to perform collision checks
        /// </summary>
        internal protected Circle circle;

        /// <summary>
        /// Create a CircleCollider
        /// </summary>
        /// <param name="gameObject">The gameObject that will update the collider coordinates when it's coordinates changes</param>
        public CircleCollider(GameObject gameObject) : base(gameObject) {}

        /// <summary>
        /// The radius of the circle
        /// </summary>
        public override float ray => circle.radius;

        /// <summary>
        /// Check if this collider collides with another one
        /// </summary>
        /// <param name="otherCollider">The second collider to check</param>
        /// <returns>True if they collide, false otherwise</returns>
        public override bool Collide(Collider otherCollider) {
            if(otherCollider.GetType().IsSubclassOf(typeof(CircleCollider))) {
                Circle circle2 = ((CircleCollider) otherCollider).circle;
                if(Utility.distance(circle.center, circle2.center)>(circle.radius + circle2.radius)){
                    return false;
                }
                return true;
            } else if(otherCollider.GetType().IsSubclassOf(typeof(ConvexPolygonCollider))) {
                return otherCollider.Collide(this);
            }
            throw new NotImplementedException("This collider has no idea how to check the collision with the other one");
        }

        /// <summary>
        /// This force the coordinates recalculation for this collider
        /// </summary>
        public override void RecreateCollider() {
            circle = new Circle();
            Move();
            circle.radius = gameObject.spriteSet.size.width > gameObject.spriteSet.size.height ? gameObject.spriteSet.size.width : gameObject.spriteSet.size.height;
        }

        /// <summary>
        /// This force the coordinates recalculation for this collider when the gameObject is moved
        /// </summary>
        /// <param name="gameObject"></param>
        protected override void Move(GameObject gameObject) {
            circle.center = gameObject.position;
        }

        /// <summary>
        /// This force the coordinates recalculation for this collider when the gameObject is rotated (since this is a circle rotating it is totally pointless, don't use this please :( )
        /// </summary>
        /// <param name="gameObject"></param>
        protected override void Rotate(GameObject gameObject) {}

        /// <summary>
        /// This method check if a point is inside the Collider
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns>True if the point is inside the Collider, false otherwise</returns>
        protected internal override bool InCollider(Point point) {
            return circle.contains(point);
        }
    }
}
