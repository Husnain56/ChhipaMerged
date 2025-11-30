using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chhipa_Motors.GUI.Admin_Panel
{
    public abstract class BookingState
    {
        public abstract string StateName { get; }
        public abstract string ButtonText { get; }
        public abstract bool CanCancel { get; }

        public abstract BookingState GetNextState();

        public virtual void OnStateEnter(BookingContext context)
        {
           
        }
    }

    public class PendingState : BookingState
    {
        public override string StateName => "Pending";
        public override string ButtonText => "Start Processing";
        public override bool CanCancel => true;

        public override BookingState GetNextState() => new ProcessingState();
    }

    public class ProcessingState : BookingState
    {
        public override string StateName => "Processing";
        public override string ButtonText => "Ship Order";
        public override bool CanCancel => false;

        public override BookingState GetNextState() => new ShippingState();
    }

    public class ShippingState : BookingState
    {
        public override string StateName => "Shipping";
        public override string ButtonText => "Mark as Shipped";
        public override bool CanCancel => false;

        public override BookingState GetNextState() => new ShippedState();
    }

    public class ShippedState : BookingState
    {
        public override string StateName => "Shipped";
        public override string ButtonText => "Mark as Delivered";
        public override bool CanCancel => false;

        public override BookingState GetNextState() => new DeliveredState();
    }

    public class DeliveredState : BookingState
    {
        public override string StateName => "Delivered";
        public override string ButtonText => "Completed";
        public override bool CanCancel => false;

        public override BookingState GetNextState() => null; 
    }

    public class CancelledState : BookingState
    {
        public override string StateName => "Cancelled";
        public override string ButtonText => "Cancelled";
        public override bool CanCancel => false;

        public override BookingState GetNextState() => null; 
    }

    public static class StateFactory
    {
        public static BookingState CreateState(string stateName)
        {
            return stateName switch
            {
                "Pending" => new PendingState(),
                "Processing" => new ProcessingState(),
                "Shipping" => new ShippingState(),
                "Shipped" => new ShippedState(),
                "Delivered" => new DeliveredState(),
                "Cancelled" => new CancelledState(),
                _ => new PendingState()
            };
        }
    }

    public class BookingContext
    {
        private BookingState _currentState;
        public int BookingId { get; set; }

        public BookingContext(string currentStateName)
        {
            _currentState = StateFactory.CreateState(currentStateName);
        }

        public BookingState CurrentState => _currentState;

        public void TransitionToNextState()
        {
            var nextState = _currentState.GetNextState();
            if (nextState != null)
            {
                _currentState = nextState;
                _currentState.OnStateEnter(this);
            }
        }

        public void Cancel()
        {
            if (_currentState.CanCancel)
            {
                _currentState = new CancelledState();
                _currentState.OnStateEnter(this);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Cannot cancel booking in {_currentState.StateName} state"
                );
            }
        }
    }
}
