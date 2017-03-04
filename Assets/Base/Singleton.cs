// http://www.c-sharpcorner.com/uploadfile/snorrebaard/the-quest-for-the-generic-singleton-in-C-Sharp/

// This is thread safe, but in theory someone could make multiple instance. You must access via Instance

public class Singleton<T> where T : class, new()
{
	protected Singleton() { }
	
	private abstract class SingletonCreator {
		static SingletonCreator() { }
		// Private object instantiated with private constructor
		internal static readonly T instance = new T();
	}
	
	public static T Instance {
		get { return SingletonCreator.instance; }
	}
}