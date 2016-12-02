using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Hylasoft.Extensions.Types
{
  /// <summary>
  /// A simple pairing of a member and attributes.
  /// </summary>
  /// <typeparam name="TAttribute">The type of attribute for the pairing.</typeparam>
  public class MemberAttributePairing<TAttribute>
  {
    public Collection<TAttribute> Attributes { get; private set; }

    public MemberInfo Member { get; private set; }

    public MemberAttributePairing(MemberInfo member, IEnumerable<TAttribute> attributes)
    {
      Attributes = (attributes == null)
        ? new Collection<TAttribute>()
        : new Collection<TAttribute>(attributes.ToArray());

      Member = member;
    }
  }
}
