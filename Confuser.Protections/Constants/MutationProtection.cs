﻿using System;
using System.Linq;
using Arithmetic_Obfuscation.Arithmetic;
using Confuser.Core;
using Confuser.Protections.Constants;
using dnlib.DotNet;

namespace Confuser.Protections {
     [AfterProtection("Ki.Constants")]
    internal class MutationProtection : Protection {
		public const string _Id = "Mutations";
		public const string _FullId = "Ki.Mutations";

		public override string Name {
			get { return "Mutation Protection"; }
		}

		public override string Description {
			get { return "This protection marks the module with a attribute that discourage ILDasm from disassembling it."; }
		}

		public override string Id {
			get { return _Id; }
		}

		public override string FullId {
			get { return _FullId; }
		}

		public override ProtectionPreset Preset {
			get { return ProtectionPreset.Minimum; }
		}

		protected override void Initialize(ConfuserContext context) {
			//
		}

		protected override void PopulatePipeline(ProtectionPipeline pipeline) {
            pipeline.InsertPostStage(PipelineStage.ProcessModule, new MutationPhase(this));
         
        }

		class MutationPhase : ProtectionPhase {
			public MutationPhase(MutationProtection parent)
				: base(parent) { }

			public override ProtectionTargets Targets {
				get { return ProtectionTargets.Modules; }
			}

			public override string Name {
				get { return "Mutations"; }
			}

			protected override void Execute(ConfuserContext context, ProtectionParameters parameters) {
				foreach (ModuleDef module in parameters.Targets.OfType<ModuleDef>()) {
                    new Arithmetic(module);
                }
			}
		}
	}
}