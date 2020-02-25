namespace FakeXrmEasy.Metadata
{
    /// <summary>
    ///   <para>Specifies the behavior of a <see cref="T:Microsoft.Xrm.Sdk.Metadata.DateTimeAttributeMetadata" /> attribute using the <see cref="P:Microsoft.Xrm.Sdk.Metadata.DateTimeAttributeMetadata.DateTimeBehavior" /> property.</para>
    /// </summary>
    public enum DateTimeAttributeBehavior
    {
        /// <summary>
        ///   <para>Stores the date and time value with current user local time zone information. Value = 1.</para>
        /// </summary>
        UserLocal = 1,

        /// <summary>
        ///   <para>Stores the date value with the time value as 12:00 AM (00:00:00) without the time zone information. Value = 2.</para>
        /// </summary>
        DateOnly = 2,

        /// <summary>
        ///   <para>Stores the date and time values without the time zone information. Value = 3.</para>
        /// </summary>
        TimeZoneIndependent = 3
    }
}