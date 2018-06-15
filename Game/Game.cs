using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Game
    {
        private World world;
        private Player player;
        public List<GameObject> Objects => world.objects;
        
        public Game()
        {

            var c = GameObjectFactory
                .GetParametrizedCharacter<Player>()
                .WithParameter<int, Health>(50, 1, 100)
                .AddCollideInteraction<Worm>((hero, apple) =>
                {
                    hero.Set<int, Health>(hero.Get<int, Health>() + apple.Get<int, NutritionalValue>());
                    apple.Die();
                })
                .CreateOnPosition(new Position(new Point(450, 200), new Size(50, 50)));
            
            var a = GameObjectFactory
                .GetParametrizedCharacter<Worm>()
                .WithParameter<int, NutritionalValue>(10, 1, 1)
                .CreateOnPosition(new Position(new Point(100, 100), new Size(20, 20)));
            
            world = new World(new GameObject[] {c, a});

            player = c;

            #region ideas
            /*
            var hero = Factory
                .GetParametrizedCharacter<Hero>()
                .OnPos(1, 2)
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100, 100)
                .AddCollideInteraction<Apple>((her, appl) =>
                    {
                        her.Set<Health>(her.Get<Health> + appl.Get<Sytnost>);
                        appl.BeEaten();
                    })
                //.AddOnTickAction((her) => { her.Set<Health>(-1) });

            var wormFactory = MetaFactory
                .GetParametrizedCharacterFactory<Hero>()
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100)
                .AddCollideInteraction<Apple>((her, appl) =>
                {
                    her.Set<Health>(her.Get < Health > +appl.Get<NutritionalValue>);
                    appl.BeEaten();
                })
                .AddOnTickAction((her) => { her.Set<Health>(-1) });
            var worm = wormFactory
                .OnPos(1, 2)
                .Set<Health>(50)
                .Set<Mana>(100)
                .CreateOnPosition();

            var apple = Factory
                .GetParametrizedCharacter<Apple>()
                .OnPos(1, 2)
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100, 100);

            worm.Get<Health>();
            c.Set<Health>();

            */

            #endregion

            //player = new Player(new Position(new Point(450, 200), new Size(50, 50)));
            //
            //var objects = new List<GameObject>
            //{
            //    player,
            //    new Worm(new Position(new Point(100, 100), new Size(10, 10))),
            //    new Worm(new Position(new Point(900, 100), new Size(10, 10))),
            //    new Worm(new Position(new Point(100, 300), new Size(10, 10))),
            //    new Worm(new Position(new Point(400, 500), new Size(10, 10)))
            //};
            //world = new World(objects.ToArray());
            //world = new World(objects.ToArray(), 
            //    new ConditionalEventWrapper[] { new Eat().Wrap() });
            //world.AddIConditionalEvent(new Eat());
        }

        public void Tick()
        {
            world.Tick();
        }

        public Player GetPlayer()
        {
            return player;
        }

        public IEnumerable<GameObject> GetGameObjectsInRectangle(Rectangle rectangle)
        {
            return world.GetObjectsInRectangle(rectangle);
        }
    }

    class Health : Parameter<int> { }
    class Mana : Parameter<double> { }
    class NutritionalValue : Parameter<int> { }

    class Apple : ParametrizedGameObject
    {
        public void Die()
        {
            IsOnMap = false;
        }
    }

    class Hero : ParametrizedGameObject
    {
        public bool MoveInDirection(Direction direction, int distance)
        {
            // проверка корректности
            Position.MoveDirection(direction, distance);
            return true;
        }
    }
}
