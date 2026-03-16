// This is the interface for the AI system, it can be scaled up or down depending on the needs of the project.
// Its main purpose is to provide a common interface for all AI implementations, allowing for easy swapping of different AI systems without changing the rest of the codebase.
// The IAI interface defines two methods: Initialize and ExecuteAI.
// If any of this 2 methods are not implemented in the class that implements this interface, it will throw an error at compile time, ensuring that all AI classes adhere to the same structure and can be used interchangeably.
public interface IAI
{
    void Initialize();
    void ExecuteAI();
}