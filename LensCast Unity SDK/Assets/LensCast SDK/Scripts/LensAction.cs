namespace LensCastSDK
{
    public class LensAction {
        public string id; //the id for the action
        public string action_name; //the name for the action
        public string value_id; //the id for the value for the action if there is a value
        public string value_name; //the name for the value for the action if there is a value

        //easy string
		public override string ToString() {
            return "ID: " + id + "\n" +
                "Name: " + action_name + "\n" +
                "ID: " + value_id + "\n" +
                "ID: " + value_name + "\n";
		}
	}
}