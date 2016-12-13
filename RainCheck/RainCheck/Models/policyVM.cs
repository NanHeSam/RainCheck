using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RainCheck.Models
{
    public class policyVM
    {
        private RainCheckConnectionString _context = new RainCheckConnectionString();
        decimal user_id;
        public policyVM(decimal user_id)
        {
            this.user_id = user_id;
        }
        public IEnumerable<policy_tbl> current_policy
        {
            get
            {
                return _context.policy_tbl
                    .Where(o => o.user_id == user_id)
                    .GroupBy(o => o.policy_number)
                    .Select(ig => ig.OrderByDescending(t => t.start_date).FirstOrDefault()).ToList();
            }
        }
        public IEnumerable<policy_tbl> policy_history
        {
            get
            {
                return _context.policy_tbl
                    .Where(o => o.user_id == user_id);
            }
        }

        public IQueryable<decimal> BodyCoverageLevel
        {
            get
            {
                var coverage = from a in _context.policy_tbl
                               where a.user_id == user_id
                               select  a.coverage_level.limite;
                return coverage;

            }
        }

        public IQueryable<decimal> PropertyDamage
        {
            get
            {
                var coverage = from a in _context.policy_tbl
                               select a.coverage_level1.limite;
                return coverage;

            }
        }


    }
}