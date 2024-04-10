using UnityEngine;

namespace UTrigger2D
{
    public static class Trgger2DAlgorithms
    {
        public static bool OnTrigger(Rect self, Rect other) =>
            self.x < other.x + other.width &&
            self.y < other.y + other.height &&
            self.x + self.width > other.x &&
            self.y + self.height > other.y;

        public static bool OnTrigger(Circle self, Circle other)
        {
            var dx = other.x - self.x;
            var dy = other.y - self.y;
            var distance = Mathf.Sqrt(dx * dx + dy * dy);

            return distance < other.radius + self.radius;
        }

        public static bool OnTrigger(Rect rect, Circle circle)
        {
            var distX = Mathf.Abs(circle.x - rect.x - rect.width / 2);
            var distY = Mathf.Abs(circle.y - rect.y - rect.height / 2);

            if (distX > (rect.width / 2 + circle.radius)) return false;
            if (distY > (rect.height / 2 + circle.radius)) return false;

            if (distX <= (rect.width / 2)) return true;
            if (distY <= (rect.height / 2)) return true;

            var dx = distX - rect.width / 2;
            var dy = distY - rect.height / 2;
            return dx * dx + dy * dy <= (circle.radius * circle.radius);
        }
    }
}